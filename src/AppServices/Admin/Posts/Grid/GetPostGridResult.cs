using DNTPersianUtils.Core;

namespace AppServices.Admin.Posts.Grid;

public class GetPostGridResult
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatedAtPersian => CreatedAt.ToShortPersianDateTimeString();
    public string UpdatedAtPersian => UpdatedAt.ToShortPersianDateTimeString();
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string? PublishedAtPersian => PublishedAt?.ToShortPersianDateTimeString();
    public string CategoryTitle { get; set; } = default!;
    public int CategoryId { get; set; }
    public int ViewCount { get; set; }
    public bool IsInEnglish { get; set; }
}
