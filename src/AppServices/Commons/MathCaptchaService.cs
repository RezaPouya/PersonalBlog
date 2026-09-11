using Microsoft.Extensions.Caching.Memory;

namespace AppServices.Commons;

/// <summary>
/// کپچای ریاضی ساده با ذخیره در کش سرور.
/// در صورت نیاز به reCAPTCHA گوگل، این کلاس را با RecaptchaService جایگزین کنید.
/// </summary>
public class MathCaptchaService(IMemoryCache cache) : ICaptchaService
{
    public Task<(string CaptchaId, string Question)> GenerateChallengeAsync(CancellationToken cancellationToken)
    {
        var random = new Random();
        var a = random.Next(1, 20);
        var b = random.Next(1, 20);
        var useMinus = random.Next(2) == 1;

        int answer;
        string question;

        if (useMinus)
        {
            if (a < b) (a, b) = (b, a);
            answer = a - b;
            question = $"{a} − {b} = ?";
        }
        else
        {
            answer = a + b;
            question = $"{a} + {b} = ?";
        }

        var captchaId = Guid.NewGuid().ToString("N");
        cache.Set($"captcha:{captchaId}", answer, TimeSpan.FromMinutes(3));

        return Task.FromResult((captchaId, question));
    }

    public Task<bool> ValidateAsync(string captchaId, string answer, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(captchaId) || string.IsNullOrWhiteSpace(answer))
            return Task.FromResult(false);

        var key = $"captcha:{captchaId}";
        if (!cache.TryGetValue(key, out int expected))
            return Task.FromResult(false);

        cache.Remove(key); // یک‌بار مصرف

        var isValid = int.TryParse(answer.Trim(), out var userAnswer) && userAnswer == expected;
        return Task.FromResult(isValid);
    }
}