namespace PersonalBlog.AppServices.Dtos;

public class CreateCourseInputDto { public string Title { get; set; } = default!; public string Slug { get; set; } = default!; public string? Description { get; set; } public string? CoverImageUrl { get; set; } public bool IsPublished { get; set; } = true; }
