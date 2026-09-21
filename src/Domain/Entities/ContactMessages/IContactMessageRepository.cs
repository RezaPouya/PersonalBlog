using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.ContactMessages.Dtos;
using Utilities.Dtos;

namespace PersonalBlog.Domain.Entities.ContactMessages;

public interface IContactMessageRepository : IRepository<ContactMessage>
{
    Task<GridDataSourceResult<ContactMessageGridDto>> GetGridAsync(GridDataSourceRequest request, CancellationToken cancellationToken);
    Task<int> GetUnreadCountAsync(CancellationToken cancellationToken);
}
