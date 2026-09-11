namespace PersonalBlog.AppServices.Dtos;

public class CourseDto
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public bool IsPublished { get; set; }
    public int PostCount { get; set; }
}

public class CreateCourseInputDto
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public bool IsPublished { get; set; } = true;
}

public class UpdateCourseInputDto : CreateCourseInputDto
{
    public long Id { get; set; }
}
