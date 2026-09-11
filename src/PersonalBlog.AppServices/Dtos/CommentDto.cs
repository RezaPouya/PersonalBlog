namespace PersonalBlog.AppServices.Dtos;

public class CommentDto
{
    public long Id { get; set; }
    public string DisplayName { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public bool IsApproved { get; set; }
    public List<CommentDto> Replies { get; set; } = new();
}

public class CreateCommentInputDto
{
    public long PostId { get; set; }
    public long? ParentCommentId { get; set; }
    public string DisplayName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string RecaptchaToken { get; set; } = default!;
}
