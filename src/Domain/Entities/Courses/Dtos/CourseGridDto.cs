namespace PersonalBlog.Domain.Entities.Courses.Dtos;

public class CourseGridDto
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public string Slug { get; set; } = default!;

    public string TinyUrl { get; set; } = default!;

    public string? Description { get; set; }

    public string? CoverImageUrl { get; set; }

    public bool IsPublished { get; set; }

    public bool IsInEnglish { get; set; }

    public int OrderInCourses { get; set; }

    public int PostsCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}