using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Projects.Dtos;
using Utilities.Dtos;

namespace PersonalBlog.Domain.Entities.Projects;

public interface IProjectRepository : IRepository<Project>
{
    Task<GridDataSourceResult<ProjectGridDto>> GetGridAsync(GridDataSourceRequest request, CancellationToken cancellationToken);
    Task<ProjectDto?> GetInfoByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> IsExistsBySlugAsync(string slug, int? id, CancellationToken cancellationToken);
    Task<List<ProjectSiteDto>> GetListForSiteAsync(CancellationToken cancellationToken);
}
