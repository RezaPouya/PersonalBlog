using DNTPersianUtils.Core;

namespace PersonalBlog.Domain.Entities.Categories.Dtos;

public class CategoryDot
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string CreatedAtPersian => CreatedAt.ToShortPersianDateTimeString();
    public string UpdatedAtPersian => UpdatedAt.ToShortPersianDateTimeString();
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string TinyUrl { get; set; }
    public string? Description { get; set; }
    public bool IsInEnglish { get; set; }
    public int PostsCount { get; set; }
}
