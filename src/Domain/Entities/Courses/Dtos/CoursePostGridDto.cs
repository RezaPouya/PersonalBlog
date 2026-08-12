namespace PersonalBlog.Domain.Entities.Courses.Dtos;

public class CoursePostGridDto
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int PostId { get; set; }

    public string PostTitle { get; set; } = default!;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? CoverImageUrl { get; set; }

    public bool IsPublished { get; set; }

    public int OrderInCourse { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}