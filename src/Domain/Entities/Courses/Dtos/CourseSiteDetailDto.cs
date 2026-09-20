namespace PersonalBlog.Domain.Entities.Courses.Dtos;

public class CourseSiteDetailDto
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public List<CoursePostSiteDto> Posts { get; set; } = new();
}

public class CoursePostSiteDto
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string PostSlug { get; set; } = default!;
    public int OrderInCourse { get; set; }
}
