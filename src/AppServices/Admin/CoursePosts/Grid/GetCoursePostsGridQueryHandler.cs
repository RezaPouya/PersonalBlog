using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Entities.Courses.Dtos;
using PersonalBlog.Utilities.Dtos;

namespace AppServices.Admin.CoursePosts.Grid;

public class GetCoursePostsGridQueryHandler(
    ICoursePostRepository coursePostRepository)
    : IQueryHandler<
        GetCoursePostsGridQuery,
        GridDataSourceResult<CoursePostGridDto>>
{
    public async Task<GridDataSourceResult<CoursePostGridDto>> Invoke(
        GetCoursePostsGridQuery input,
        CancellationToken cancellationToken)
    {
        return await coursePostRepository.GetGridAsync(
            input.CourseId,
            input,
            cancellationToken);
    }
}