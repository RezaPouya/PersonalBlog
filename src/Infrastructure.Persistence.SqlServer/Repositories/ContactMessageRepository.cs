using PersonalBlog.Domain.Entities.ContactMessages;
using PersonalBlog.Domain.Entities.ContactMessages.Dtos;
using Utilities.Dtos;

namespace Infrastructure.Persistence.SqlServer.Repositories;

public class ContactMessageRepository(AppDbContext dbContext) : RepositoryBase<ContactMessage>(dbContext), IContactMessageRepository
{
    public async Task<GridDataSourceResult<ContactMessageGridDto>> GetGridAsync(GridDataSourceRequest request, CancellationToken cancellationToken)
    {
        var query = base.DbContext.ContactMessages.AsNoTracking()
            .Select(c => new ContactMessageGridDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                Cellphone = c.Cellphone,
                Subject = c.Subject,
                Body = c.Body,
                IsRead = c.IsRead,
                CreatedAt = c.CreatedAt
            });

        return await query.ToDataSourceResult(request, cancellationToken);
    }

    public async Task<int> GetUnreadCountAsync(CancellationToken cancellationToken)
    {
        return await base.DbContext.ContactMessages.AsNoTracking().CountAsync(c => !c.IsRead, cancellationToken);
    }
}
