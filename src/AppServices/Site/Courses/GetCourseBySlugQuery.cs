using PersonalBlog.Domain.Entities.Courses.Dtos;

namespace AppServices.Site.Courses;

public class GetCourseBySlugQuery : IQuery<CourseSiteDetailDto?>
{
    public string Slug { get; set; } = default!;
}
