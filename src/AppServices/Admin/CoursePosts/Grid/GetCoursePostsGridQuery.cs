using PersonalBlog.Domain.Entities.Courses.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.CoursePosts.Grid;

public class GetCoursePostsGridQuery : GridDataSourceRequest, IQuery<GridDataSourceResult<CoursePostGridDto>>
{
    public int CourseId { get; set; }
}