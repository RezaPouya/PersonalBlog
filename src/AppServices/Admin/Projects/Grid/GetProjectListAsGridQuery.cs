using PersonalBlog.Domain.Entities.Projects.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.Projects.Grid;

public class GetProjectListAsGridQuery : GridDataSourceRequest, IQuery<GridDataSourceResult<ProjectGridDto>>
{
}
