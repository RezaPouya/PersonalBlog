using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Entities.Courses.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.Courses.Grid;

public class GetCourseListAsGridQueryHandler(ICourseRepository courseRepository) : IQueryHandler<GetCourseListAsGridQuery, GridDataSourceResult<CourseGridDto>>
{
    public async Task<GridDataSourceResult<CourseGridDto>> Handle(GetCourseListAsGridQuery input, CancellationToken cancellationToken)
    {
        return await courseRepository.GetGridAsync(input, cancellationToken);
    }
}