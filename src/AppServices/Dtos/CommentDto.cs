namespace PersonalBlog.AppServices.Dtos;

// --- Comments & Messages ---
public class CommentDto { public long Id { get; set; } public string DisplayName { get; set; } = default!; public string Content { get; set; } = default!; public DateTime CreatedAt { get; set; } public bool IsApproved { get; set; } public List<CommentDto> Replies { get; set; } = new(); }
