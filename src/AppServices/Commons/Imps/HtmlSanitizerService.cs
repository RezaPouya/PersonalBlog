using Ganss.Xss;

namespace AppServices.Commons.Imps;

public sealed class HtmlSanitizerService : IHtmlSanitizerService
{
    private readonly HtmlSanitizer _sanitizer;

    public HtmlSanitizerService()
    {
        _sanitizer = new HtmlSanitizer();

        // فقط schemeهای مورد نیاز Blog
        _sanitizer.AllowedSchemes.Clear();
        _sanitizer.AllowedSchemes.Add("http");
        _sanitizer.AllowedSchemes.Add("https");

        // class را فقط اگر واقعاً برای syntax highlighting
        // یا editor لازم داریم اضافه می‌کنیم.
        _sanitizer.AllowedAttributes.Add("class");
    }

    public string Sanitize(string html)
    {
        return _sanitizer.Sanitize(html);
    }
}