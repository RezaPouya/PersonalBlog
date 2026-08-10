namespace PersonalBlog.Domain.Entities.Posts;

public class Comment : EntityBase
{
    public int PostId { get; set; }
    public Post Post { get; set; } = default!;

    public int? ParentCommentId { get; set; }
    public Comment? ParentComment { get; set; }
    public ICollection<Comment> Replies { get; set; } = new List<Comment>();

    public string DisplayName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Content { get; set; } = default!;

    public bool IsApproved { get; set; }
    public bool IsSpam { get; set; }

    public string? IpAddress { get; set; }
}
