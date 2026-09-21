using PersonalBlog.Domain.Entities.Projects;
using PersonalBlog.Domain.Entities.Projects.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.Projects.Grid;

public class GetProjectListAsGridQueryHandler(IProjectRepository projectRepository)
    : IQueryHandler<GetProjectListAsGridQuery, GridDataSourceResult<ProjectGridDto>>
{
    public async Task<GridDataSourceResult<ProjectGridDto>> Handle(GetProjectListAsGridQuery input, CancellationToken cancellationToken)
    {
        return await projectRepository.GetGridAsync(input, cancellationToken);
    }
}
