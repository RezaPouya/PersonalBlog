using DNTPersianUtils.Core;

namespace PersonalBlog.Domain.Entities.Comments.Dtos;

public class CommentGridDto
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string PostTitle { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Content { get; set; } = default!;
    public bool IsApproved { get; set; }
    public bool IsSpam { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedAtPersian => CreatedAt.ToShortPersianDateTimeString();
}
