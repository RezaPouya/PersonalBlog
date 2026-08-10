using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Entities;

public class Comment : EntityBase
{
    public long PostId { get; set; }
    public Post Post { get; set; } = default!;

    public long? ParentCommentId { get; set; }
    public Comment? ParentComment { get; set; }
    public ICollection<Comment> Replies { get; set; } = new List<Comment>();

    public string DisplayName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Content { get; set; } = default!;

    public bool IsApproved { get; set; }
    public bool IsSpam { get; set; }

    public string? IpAddress { get; set; }
}
