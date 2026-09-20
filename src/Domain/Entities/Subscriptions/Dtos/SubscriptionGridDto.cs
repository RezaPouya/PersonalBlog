using DNTPersianUtils.Core;

namespace PersonalBlog.Domain.Entities.Subscriptions.Dtos;

public class SubscriptionGridDto
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime? UnsubscribedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedAtPersian => CreatedAt.ToShortPersianDateTimeString();
}
