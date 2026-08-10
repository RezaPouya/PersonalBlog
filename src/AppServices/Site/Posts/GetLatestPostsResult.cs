namespace AppServices.Site.Posts;

public class GetLatestPostsResult
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string CategoryTitle { get; set; } = default!;
    public int ViewCount { get; set; }
}
