namespace PersonalBlog.AppServices.Dtos;

public class ContactMessageDto { public long Id { get; set; } public string FullName { get; set; } = default!; public string Email { get; set; } = default!; public string Subject { get; set; } = default!; public string Body { get; set; } = default!; public bool IsRead { get; set; } public DateTime CreatedAt { get; set; } }
