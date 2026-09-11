using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Services.Blog;

public interface ITagService
{
    Task<List<TagDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TagDto?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<long> GetOrCreateAsync(string title, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
