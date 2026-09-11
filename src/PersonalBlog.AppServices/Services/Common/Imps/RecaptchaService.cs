using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalBlog.AppServices.Options;

namespace PersonalBlog.AppServices.Services.Common.Imps;

/// <summary>پیاده‌سازی reCAPTCHA v2/v3 (siteverify) با HttpClient.</summary>
public class RecaptchaService(
    IHttpClientFactory httpClientFactory,
    IOptions<RecaptchaSettings> options,
    ILogger<RecaptchaService> logger) : ICaptchaService
{
    private readonly RecaptchaSettings _settings = options.Value;

    public async Task<bool> ValidateAsync(string token, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(token))
            return false;

        try
        {
            var client = httpClientFactory.CreateClient("recaptcha");

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["secret"] = _settings.SecretKey,
                ["response"] = token
            });

            using var response = await client.PostAsync(
                "https://www.google.com/recaptcha/api/siteverify", content, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var success = root.TryGetProperty("success", out var successEl) && successEl.GetBoolean();
            if (!success)
                return false;

            // برای v3، امتیاز اسپم را هم بررسی کن (اگر موجود بود)
            if (root.TryGetProperty("score", out var scoreEl))
                return scoreEl.GetDouble() >= _settings.MinimumScore;

            return true;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "خطا در اعتبارسنجی reCAPTCHA");
            return false;
        }
    }
}
