using AppServices.Commons;
using AppServices.Options;
using Microsoft.Extensions.Options;

namespace Web.Services;

/// <summary>
/// ذخیره‌سازی فایل روی دیسک محلی، زیر wwwroot/uploads/{subFolder}.
/// مناسب برای اجرا روی یک سرور/VPS ثابت (نه پلتفرم Serverless با دیسک موقت).
/// </summary>
public sealed class LocalFileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly FileStorageSettings _settings;

    private const string UploadsRootFolder = "uploads";

    public LocalFileStorageService(IWebHostEnvironment env, IOptions<FileStorageSettings> options)
    {
        _env = env;
        _settings = options.Value;
    }

    public bool IsAllowedImage(string fileName, long fileSizeBytes)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return false;

        var extension = Path.GetExtension(fileName).ToLowerInvariant();

        if (!_settings.AllowedImageExtensions.Contains(extension))
            return false;

        if (fileSizeBytes <= 0 || fileSizeBytes > _settings.MaxImageSizeBytes)
            return false;

        return true;
    }

    public async Task<string> SaveAsync(Stream content, string originalFileName, string subFolder, CancellationToken cancellationToken = default)
    {
        if (content is null)
            throw new ArgumentNullException(nameof(content));

        subFolder = SanitizeFolderName(subFolder);

        var extension = Path.GetExtension(originalFileName).ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(extension))
            extension = ".bin";

        // نام فایل تصادفی و امن، تا هم تداخل نام پیش نیاید و هم کاربر نتواند
        // با نام فایل مسیر دلخواه (Path Traversal) تزریق کند.
        var safeFileName = $"{Guid.NewGuid():N}{extension}";

        var physicalFolder = Path.Combine(_env.WebRootPath, UploadsRootFolder, subFolder);
        Directory.CreateDirectory(physicalFolder);

        var physicalPath = Path.Combine(physicalFolder, safeFileName);

        await using (var fileStream = new FileStream(physicalPath, FileMode.CreateNew, FileAccess.Write))
        {
            await content.CopyToAsync(fileStream, cancellationToken);
        }

        // آدرس نسبی که هم در <img src> سایت و هم در دیتابیس ذخیره می‌شود.
        return $"/{UploadsRootFolder}/{subFolder}/{safeFileName}";
    }

    public void Delete(string? relativeUrl)
    {
        if (string.IsNullOrWhiteSpace(relativeUrl))
            return;

        // فقط فایل‌هایی که خودمان از همین مسیر ساخته‌ایم پاک می‌شوند؛
        // لینک‌های خارجی (که کاربر مستقیم در فیلد URL وارد کرده) دست‌نخورده می‌مانند.
        if (!relativeUrl.StartsWith($"/{UploadsRootFolder}/", StringComparison.OrdinalIgnoreCase))
            return;

        var relativePath = relativeUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
        var physicalPath = Path.Combine(_env.WebRootPath, relativePath);

        // اطمینان از اینکه مسیر نهایی همچنان داخل wwwroot/uploads است (دفاع در برابر Path Traversal)
        var uploadsRoot = Path.Combine(_env.WebRootPath, UploadsRootFolder);
        var fullPath = Path.GetFullPath(physicalPath);
        if (!fullPath.StartsWith(Path.GetFullPath(uploadsRoot), StringComparison.OrdinalIgnoreCase))
            return;

        if (File.Exists(fullPath))
        {
            try
            {
                File.Delete(fullPath);
            }
            catch (IOException)
            {
                // فایل قفل بود یا در دسترس نبود؛ نیازی به متوقف‌کردن جریان اصلی برنامه نیست.
            }
        }
    }

    private static string SanitizeFolderName(string subFolder)
    {
        if (string.IsNullOrWhiteSpace(subFolder))
            return "misc";

        var invalidChars = Path.GetInvalidFileNameChars();
        var cleaned = new string(subFolder.Where(c => !invalidChars.Contains(c) && c != '/' && c != '\\').ToArray());

        return string.IsNullOrWhiteSpace(cleaned) ? "misc" : cleaned.ToLowerInvariant();
    }
}
