using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Courses.Dtos;
using PersonalBlog.Utilities.Dtos;

namespace PersonalBlog.Domain.Entities.Courses;

public interface ICoursePostRepository : IRepository<CoursePost>
{
    Task<bool> IsExistsAsync(
        int courseId,
        int postId,
        int? id,
        CancellationToken cancellationToken);

    Task<bool> IsPostAlreadyInCourseAsync(
        int courseId,
        int postId,
        int? id,
        CancellationToken cancellationToken);

    Task<int> GetNextOrderAsync(
        int courseId,
        CancellationToken cancellationToken);

    Task<GridDataSourceResult<CoursePostGridDto>> GetGridAsync(
        int courseId,
        GridDataSourceRequest request,
        CancellationToken cancellationToken);
}