using DNTPersianUtils.Core;

namespace AppServices.Admin.Categories.Grid;

public class GetCategoryGridResult
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string CreatedAtPersian => CreatedAt.ToShortPersianDateTimeString();
    public string UpdatedAtPersian => UpdatedAt.ToShortPersianDateTimeString();

    public string Title { get; set; } = default!;
    public bool IsInEnglish { get; set; }

    public int PostsCount { get; set; }
}