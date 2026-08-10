using DNTPersianUtils.Core;

namespace AppServices.Admin.Posts.Read;

public class GetPostResult
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatedAtPersian => CreatedAt.ToShortPersianDateTimeString();
    public string UpdatedAtPersian => UpdatedAt.ToShortPersianDateTimeString();
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }
    public int CategoryId { get; set; }
    public string CategoryTitle { get; set; } = default!;
    public bool IsCommentsEnabled { get; set; }
    public bool IsInEnglish { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgImageUrl { get; set; }
    public int ViewCount { get; set; }
    public string TinyUrl { get; set; } = default!;
    public List<int> TagIds { get; set; } = new();
}
