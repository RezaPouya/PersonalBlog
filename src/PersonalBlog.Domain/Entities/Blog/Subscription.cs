using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Entities.Blog;

public class Subscription : EntityBase
{
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public DateTime? UnsubscribedAt { get; set; }
}
