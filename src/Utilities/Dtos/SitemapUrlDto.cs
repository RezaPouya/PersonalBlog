namespace Utilities.Dtos;

/// <summary>یک آدرس ساده برای sitemap.xml (مستقل از هر دامنه‌ی خاص).</summary>
public class SitemapUrlDto
{
    public string Slug { get; set; } = default!;
    public DateTime UpdatedAt { get; set; }
}
