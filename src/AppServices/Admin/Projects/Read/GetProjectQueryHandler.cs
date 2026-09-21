using PersonalBlog.Domain.Entities.Projects;
using PersonalBlog.Domain.Entities.Projects.Dtos;

namespace AppServices.Admin.Projects.Read;

public class GetProjectQueryHandler(IProjectRepository projectRepository)
    : IQueryHandler<GetProjectQuery, ProjectDto?>
{
    public async Task<ProjectDto?> Handle(GetProjectQuery input, CancellationToken cancellationToken)
    {
        return await projectRepository.GetInfoByIdAsync(input.Id, cancellationToken);
    }
}
