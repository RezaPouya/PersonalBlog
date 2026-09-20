using DNTPersianUtils.Core;

namespace PersonalBlog.Domain.Entities.ContactMessages.Dtos;

public class ContactMessageGridDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Cellphone { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Body { get; set; } = default!;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedAtPersian => CreatedAt.ToShortPersianDateTimeString();
}
