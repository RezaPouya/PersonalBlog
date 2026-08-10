namespace PersonalBlog.Domain.Exceptions;

public class AppExceptionLog
{
    public AppExceptionLog()
    {
        this.CreatedAt = DateTime.Now;
    }

    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Message { get; set; } = default!;
    public string? ExceptionType { get; set; }
    public string? StackTrace { get; set; }
    public string? Source { get; set; }
    public string? RequestPath { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public int? UserId { get; set; }
}
