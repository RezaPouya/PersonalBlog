using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Entities;

public class Subscription : EntityBase
{
    public Subscription()
    {
        UniqueCode = Guid.CreateVersion7().ToString("N").ToLower();
    }

    public string Email { get; set; } = default!;
    public string UniqueCode { get; set; }
    public string? UnsubscribedReason { get; set; }
    public string? UnsubscribedDescription { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? UnsubscribedAt { get; set; }
}
