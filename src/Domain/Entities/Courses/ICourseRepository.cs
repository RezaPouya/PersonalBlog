using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Courses.Dtos;
using PersonalBlog.Utilities.Dtos;
using Utilities.Dtos;

namespace PersonalBlog.Domain.Entities.Courses;

public interface ICourseRepository : IRepository<Course>
{
    Task<bool> IsExistsByTitleAsync(
        string title,
        int? id,
        CancellationToken cancellationToken);

    Task<bool> IsExistsBySlugAsync(
        string slug,
        int? id,
        CancellationToken cancellationToken);

    Task<bool> HasAnyPostAsync(
        int courseId,
        CancellationToken cancellationToken);

    Task<GridDataSourceResult<CourseGridDto>> GetGridAsync(
        GridDataSourceRequest request,
        CancellationToken cancellationToken);

    Task<List<IdTitleDto<int>>> GetListForLookupAsync(
        CancellationToken cancellationToken);
}