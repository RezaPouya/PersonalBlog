namespace PersonalBlog.AppServices.Options;

public class EmailSettings
{
    public string Host { get; set; } = default!;
    public int Port { get; set; }
    public bool EnableSsl { get; set; } = true;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string FromAddress { get; set; } = default!;
    public string FromDisplayName { get; set; } = default!;
    public string AdminNotificationAddress { get; set; } = default!;
}
