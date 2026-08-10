namespace PersonalBlog.Domain.Entities.Posts.Entities;

/// <summary>
/// ثبت جزئیات هر بازدید؛ فیلدهای مرورگر/سیستم‌عامل/دستگاه با UAParser استخراج می‌شوند.
/// </summary>
public class PostVisit : EntityBase
{
    public int PostId { get; set; }
    public Post Post { get; set; } = default!;

    public DateTime VisitedAt { get; set; } = DateTime.Now;

    public string IpAddress { get; set; } = default!;   // max 45 (IPv6)
    public string? UserAgent { get; set; }               // max 512
    public string? Browser { get; set; }                 // max 100
    public string? OS { get; set; }                      // max 100
    public string? Device { get; set; }                  // max 100
}
