namespace AppServices.Options;

public class FileStorageSettings
{
    /// <summary>حداکثر حجم مجاز هر تصویر (پیش‌فرض ۵ مگابایت).</summary>
    public long MaxImageSizeBytes { get; set; } = 5 * 1024 * 1024;

    /// <summary>پسوندهای مجاز برای تصاویر.</summary>
    public string[] AllowedImageExtensions { get; set; } =
        [".jpg", ".jpeg", ".png", ".webp", ".gif"];
}
