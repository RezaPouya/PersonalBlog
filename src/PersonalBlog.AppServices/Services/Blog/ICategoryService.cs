using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Services.Blog;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CategoryDto?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<long> CreateAsync(CreateCategoryInputDto input, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateCategoryInputDto input, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
