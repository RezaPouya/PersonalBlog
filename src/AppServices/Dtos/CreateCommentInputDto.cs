namespace AppServices.Dtos;

public class CreateCommentInputDto { public long PostId { get; set; } public long? ParentCommentId { get; set; } public string DisplayName { get; set; } = default!; public string Email { get; set; } = default!; public string Content { get; set; } = default!; public string RecaptchaToken { get; set; } = default!; }
