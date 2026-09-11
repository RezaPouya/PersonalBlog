using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Services.Blog;

public interface IContactMessageService
{
    Task<long> SubmitAsync(CreateContactMessageInputDto input, string ipAddress,
        CancellationToken cancellationToken = default);

    Task<List<ContactMessageDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task MarkAsReadAsync(long id, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
