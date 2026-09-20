using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Entities.Courses.Dtos;

namespace AppServices.Site.Courses;

public class GetCourseBySlugQueryHandler(ICourseRepository courseRepository)
    : IQueryHandler<GetCourseBySlugQuery, CourseSiteDetailDto?>
{
    public async Task<CourseSiteDetailDto?> Handle(GetCourseBySlugQuery input, CancellationToken cancellationToken)
    {
        return await courseRepository.GetPublishedBySlugAsync(input.Slug, cancellationToken);
    }
}
