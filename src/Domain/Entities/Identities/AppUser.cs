global using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.Domain.Entities.Identities;

/// <summary>
/// فقط برای ادمین سایت استفاده می‌شود؛ بازدیدکننده‌ها نیازی به اکانت ندارند.
/// </summary>
public class AppUser : IdentityUser<int>
{
    public string? DisplayName { get; set; }
    public string? AvatarUrl { get; set; }
}
