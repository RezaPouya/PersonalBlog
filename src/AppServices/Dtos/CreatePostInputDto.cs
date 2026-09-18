namespace AppServices.Dtos;

public class CreatePostInputDto
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public bool IsPublished { get; set; }
    public long CategoryId { get; set; }
    public long? CourseId { get; set; }
    public int? OrderInCourse { get; set; }
    public List<long> TagIds { get; set; } = new();
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgImageUrl { get; set; }
}
