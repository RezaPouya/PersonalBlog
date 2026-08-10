using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Exceptions;

public class AppExceptionLog : EntityBase
{
    public long Id { get; set; }
    public string Message { get; set; } = default!;
    public string? ExceptionType { get; set; }
    public string? StackTrace { get; set; }
    public string? Source { get; set; }
    public string? RequestPath { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public long? UserId { get; set; }
}
