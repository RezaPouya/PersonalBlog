using DNTPersianUtils.Core;

namespace PersonalBlog.Domain.Entities.Comments.Dtos;

/// <summary>یک کامنت تأیید‌شده برای نمایش عمومی زیر پست.</summary>
public class PublicCommentDto
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public string CreatedAtPersian => CreatedAt.ToShortPersianDateTimeString();
    public List<PublicCommentDto> Replies { get; set; } = new();
}
