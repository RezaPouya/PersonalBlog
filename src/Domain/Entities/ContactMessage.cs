using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Entities;

public class ContactMessage : EntityBase
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Cellphone { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Body { get; set; } = default!;

    public bool IsRead { get; set; }
    public string? IpAddress { get; set; }
}
