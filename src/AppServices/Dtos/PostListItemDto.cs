namespace PersonalBlog.AppServices.Dtos;

// --- Posts ---
public class PostListItemDto { public long Id { get; set; } public string Title { get; set; } = default!; public string Slug { get; set; } = default!; public string Summary { get; set; } = default!; public string? CoverImageUrl { get; set; } public bool IsPublished { get; set; } public DateTime? PublishedAt { get; set; } public int ViewCount { get; set; } public string CategoryTitle { get; set; } = default!; public List<string> Tags { get; set; } = new(); }
