using PersonalBlog.Domain.Entities.Projects;
using PersonalBlog.Domain.Entities.Projects.Dtos;

namespace AppServices.Site.Projects;

public class GetProjectsForSiteQueryHandler(IProjectRepository projectRepository)
    : IQueryHandler<GetProjectsForSiteQuery, List<ProjectSiteDto>>
{
    public async Task<List<ProjectSiteDto>> Handle(GetProjectsForSiteQuery input, CancellationToken cancellationToken)
    {
        return await projectRepository.GetListForSiteAsync(cancellationToken);
    }
}
