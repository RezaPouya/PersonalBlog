using PersonalBlog.Domain.Entities.Courses;

namespace AppServices.Admin.Courses.Read;

public class GetCourseQueryHandler(ICourseRepository courseRepository)
    : IQueryHandler<GetCourseQuery, CourseHeaderDto?>
{
    public async Task<CourseHeaderDto?> Handle(GetCourseQuery input, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetByIdAsync(input.Id, cancellationToken);
        if (course is null) return null;

        return new CourseHeaderDto { Id = course.Id, Title = course.Title };
    }
}
