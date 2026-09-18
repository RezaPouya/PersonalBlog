namespace AppServices.Dtos;

public class PostDetailDto : PostListItemDto { public string Content { get; set; } = default!; public long CategoryId { get; set; } public long? CourseId { get; set; } public string? CourseTitle { get; set; } public string? MetaTitle { get; set; } public string? MetaDescription { get; set; } public string? OgImageUrl { get; set; } public List<CommentDto> Comments { get; set; } = new(); }
