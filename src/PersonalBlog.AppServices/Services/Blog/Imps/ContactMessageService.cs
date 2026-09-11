using Microsoft.EntityFrameworkCore;
using PersonalBlog.AppServices.Dtos;
using PersonalBlog.AppServices.Services.Common;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Blog;

namespace PersonalBlog.AppServices.Services.Blog.Imps;

public class ContactMessageService(
    IRepository<ContactMessage> repository,
    IUnitOfWork unitOfWork,
    ICaptchaService captchaService,
    IEmailService emailService) : IContactMessageService
{
    public async Task<long> SubmitAsync(CreateContactMessageInputDto input, string ipAddress,
        CancellationToken cancellationToken = default)
    {
        var captchaOk = await captchaService.ValidateAsync(input.RecaptchaToken, cancellationToken);
        if (!captchaOk)
            throw new InvalidOperationException("اعتبارسنجی کپچا ناموفق بود.");

        var entity = new ContactMessage
        {
            FullName = input.FullName,
            Email = input.Email,
            Subject = input.Subject,
            Body = input.Body,
            IpAddress = ipAddress
        };

        repository.Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await emailService.SendToAdminAsync(
            $"پیام جدید از فرم تماس: {input.Subject}",
            $"<p><b>از طرف:</b> {input.FullName} ({input.Email})</p><p>{input.Body}</p>",
            cancellationToken);

        return entity.Id;
    }

    public async Task<List<ContactMessageDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await repository.Query()
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new ContactMessageDto
            {
                Id = m.Id,
                FullName = m.FullName,
                Email = m.Email,
                Subject = m.Subject,
                Body = m.Body,
                IsRead = m.IsRead,
                CreatedAt = m.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }

    public async Task MarkAsReadAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(id);
        if (entity is null) return;

        entity.IsRead = true;
        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(id);
        if (entity is null) return;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
