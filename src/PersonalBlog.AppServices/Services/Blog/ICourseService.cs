using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Services.Blog;

public interface ICourseService
{
    Task<List<CourseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CourseDto?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<long> CreateAsync(CreateCourseInputDto input, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateCourseInputDto input, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
