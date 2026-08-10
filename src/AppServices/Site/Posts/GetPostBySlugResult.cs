namespace AppServices.Site.Posts;

public class GetPostBySlugResult
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string CategoryTitle { get; set; } = default!;
    public string CategorySlug { get; set; } = default!;
    public int ViewCount { get; set; }
    public bool IsCommentsEnabled { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public List<string> Tags { get; set; } = new();
}
