namespace PersonalBlog.Domain.Entities.Posts.Dtos;

/// <summary>خلاصه‌ی یک پست برای نمایش در لیست‌ها (خانه، آرشیو، دسته‌بندی، تگ).</summary>
public class PostSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string CategoryTitle { get; set; } = default!;
    public string CategorySlug { get; set; } = default!;
    public int ViewCount { get; set; }
}
