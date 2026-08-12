namespace AppServices.Commons;

public interface IHtmlSanitizerService
{
    string Sanitize(string html);
}
