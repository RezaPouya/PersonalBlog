using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Exceptions;

/// <summary>
/// ثبت خطاهای اپلیکیشن در همان دیتابیس اصلی (طبق درخواست کاربر، بدون سرویس Monitoring جدا).
/// </summary>
public class AppExceptionLog : EntityBase
{
    public string Message { get; set; } = default!;
    public string? ExceptionType { get; set; }
    public string? StackTrace { get; set; }
    public string? Source { get; set; }
    public string? RequestPath { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public long? UserId { get; set; }
}
