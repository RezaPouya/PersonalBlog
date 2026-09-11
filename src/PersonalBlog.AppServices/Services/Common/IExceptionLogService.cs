namespace PersonalBlog.AppServices.Services.Common;

/// <summary>
/// ثبت خطاها در همان دیتابیس اصلی (جدول AppExceptionLogs) - طبق درخواست کاربر،
/// بدون سرویس Monitoring جداگانه.
/// </summary>
public interface IExceptionLogService
{
    Task LogAsync(Exception exception, string? requestPath = null, string? ipAddress = null,
        string? userAgent = null, long? userId = null, CancellationToken cancellationToken = default);
}
