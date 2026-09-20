using PersonalBlog.Domain.Entities.Projects.Dtos;

namespace AppServices.Admin.Projects.Read;

public class GetProjectQuery : IQuery<ProjectDto?>
{
    public int Id { get; set; }
}
