using PersonalBlog.Utilities.Dtos;

namespace AppServices.Admin.CoursePosts.Grid;

public class GetCoursePostsGridQuery : GridDataSourceRequest
{
    public int CourseId { get; set; }
}