namespace AppServices.Commons;

/// <summary>
/// انتزاعی برای ذخیره و مدیریت فایل‌های آپلودی (مثل تصویر کاور پست).
/// پیاده‌سازی فعلی روی دیسک محلی (wwwroot) است؛ در صورت نیاز به آبجکت‌استوریج
/// ابری در آینده، فقط کافی است یک پیاده‌سازی جدید از همین اینترفیس نوشته شود.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// بررسی می‌کند آیا فایل ارسالی از نظر پسوند و حجم مجاز است یا نه.
    /// </summary>
    bool IsAllowedImage(string fileName, long fileSizeBytes);

    /// <summary>
    /// محتوای استریم را ذخیره می‌کند و آدرس نسبی قابل‌نمایش (مثلاً /uploads/posts/xxx.jpg) را برمی‌گرداند.
    /// </summary>
    Task<string> SaveAsync(Stream content, string originalFileName, string subFolder, CancellationToken cancellationToken = default);

    /// <summary>
    /// فایل مرتبط با یک آدرس نسبی قبلاً ذخیره‌شده را حذف می‌کند (در صورت وجود).
    /// آدرس‌هایی که از این سرویس نیامده باشند (مثلاً لینک خارجی) نادیده گرفته می‌شوند.
    /// </summary>
    void Delete(string? relativeUrl);
}
