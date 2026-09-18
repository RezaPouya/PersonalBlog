using PersonalBlog.Domain.Entities.Courses.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.Courses.Grid;

public class GetCourseListAsGridQuery : GridDataSourceRequest, IQuery<GridDataSourceResult<CourseGridDto>>
{
}