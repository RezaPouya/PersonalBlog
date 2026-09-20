namespace PersonalBlog.Domain.Entities.Courses.Dtos;

public class CourseSiteListDto
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public int PostsCount { get; set; }
}
