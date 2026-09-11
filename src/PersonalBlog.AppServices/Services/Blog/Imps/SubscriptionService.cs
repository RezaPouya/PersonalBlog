using Microsoft.EntityFrameworkCore;
using PersonalBlog.AppServices.Dtos;
using PersonalBlog.AppServices.Services.Common;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Blog;

namespace PersonalBlog.AppServices.Services.Blog.Imps;

public class SubscriptionService(
    IRepository<Subscription> repository,
    IUnitOfWork unitOfWork,
    IEmailService emailService) : ISubscriptionService
{
    public async Task SubscribeAsync(CreateSubscriptionInputDto input, CancellationToken cancellationToken = default)
    {
        var existing = await repository.Query()
            .FirstOrDefaultAsync(s => s.Email == input.Email, cancellationToken);

        if (existing != null)
        {
            if (!existing.IsActive)
            {
                existing.IsActive = true;
                existing.UnsubscribedAt = null;
                repository.Update(existing);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return;
        }

        repository.Add(new Subscription { Email = input.Email, IsActive = true });
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UnsubscribeAsync(string email, CancellationToken cancellationToken = default)
    {
        var existing = await repository.Query()
            .FirstOrDefaultAsync(s => s.Email == email, cancellationToken);

        if (existing is null) return;

        existing.IsActive = false;
        existing.UnsubscribedAt = DateTime.Now;
        repository.Update(existing);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<SubscriptionDto>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await repository.Query()
            .Where(s => s.IsActive)
            .OrderByDescending(s => s.CreatedAt)
            .Select(s => new SubscriptionDto
            {
                Id = s.Id,
                Email = s.Email,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }

    public async Task SendNewsletterAsync(string subject, string htmlBody, CancellationToken cancellationToken = default)
    {
        var emails = await repository.Query()
            .Where(s => s.IsActive)
            .Select(s => s.Email)
            .ToListAsync(cancellationToken);

        await emailService.SendBulkAsync(emails, subject, htmlBody, cancellationToken);
    }
}
