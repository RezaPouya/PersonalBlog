namespace PersonalBlog.AppServices.Options;

public class RecaptchaSettings
{
    public string SiteKey { get; set; } = default!;
    public string SecretKey { get; set; } = default!;
    public double MinimumScore { get; set; } = 0.5; // برای v3
}
