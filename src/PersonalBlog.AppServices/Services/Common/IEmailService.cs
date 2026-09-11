namespace PersonalBlog.AppServices.Services.Common;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string htmlBody, CancellationToken cancellationToken = default);
    Task SendToAdminAsync(string subject, string htmlBody, CancellationToken cancellationToken = default);
    Task SendBulkAsync(IEnumerable<string> to, string subject, string htmlBody, CancellationToken cancellationToken = default);
}
