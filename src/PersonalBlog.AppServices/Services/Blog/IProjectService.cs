using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Services.Blog;

public interface IProjectService
{
    Task<List<ProjectDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<long> CreateAsync(CreateProjectInputDto input, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateProjectInputDto input, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
