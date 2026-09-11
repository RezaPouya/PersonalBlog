namespace PersonalBlog.AppServices;

/// <summary>
/// نسخه‌ی ساده‌شده؛ چون فقط یک نقش (Admin) در سایت وجود دارد.
/// </summary>
public interface ICurrentAppUser
{
    long UserId { get; }
    string UserName { get; }
    bool IsAuthenticated { get; }
    string IpAddress { get; }
    string? UserAgent { get; }

    bool IsAdmin();
}
