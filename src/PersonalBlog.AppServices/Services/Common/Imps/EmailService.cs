using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalBlog.AppServices.Options;

namespace PersonalBlog.AppServices.Services.Common.Imps;

public class EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger) : IEmailService
{
    private readonly EmailSettings _settings = options.Value;

    public async Task SendAsync(string to, string subject, string htmlBody, CancellationToken cancellationToken = default)
    {
        try
        {
            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                EnableSsl = _settings.EnableSsl,
                Credentials = new NetworkCredential(_settings.Username, _settings.Password)
            };

            using var message = new MailMessage
            {
                From = new MailAddress(_settings.FromAddress, _settings.FromDisplayName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            message.To.Add(to);

            await client.SendMailAsync(message, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ارسال ایمیل به {To} ناموفق بود", to);
        }
    }

    public Task SendToAdminAsync(string subject, string htmlBody, CancellationToken cancellationToken = default)
        => SendAsync(_settings.AdminNotificationAddress, subject, htmlBody, cancellationToken);

    public async Task SendBulkAsync(IEnumerable<string> to, string subject, string htmlBody, CancellationToken cancellationToken = default)
    {
        foreach (var email in to)
        {
            await SendAsync(email, subject, htmlBody, cancellationToken);
        }
    }
}
