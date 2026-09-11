using Microsoft.Extensions.Logging;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Exceptions;

namespace PersonalBlog.AppServices.Services.Common.Imps;

public class ExceptionLogService(
    IRepository<AppExceptionLog> repository,
    IUnitOfWork unitOfWork,
    ILogger<ExceptionLogService> logger) : IExceptionLogService
{
    public async Task LogAsync(Exception exception, string? requestPath = null, string? ipAddress = null,
        string? userAgent = null, long? userId = null, CancellationToken cancellationToken = default)
    {
        try
        {
            repository.Add(new AppExceptionLog
            {
                Message = exception.Message,
                ExceptionType = exception.GetType().FullName,
                StackTrace = exception.StackTrace,
                Source = exception.Source,
                RequestPath = requestPath,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                UserId = userId
            });

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception dbEx)
        {
            // اگر ثبت در دیتابیس هم شکست خورد، حداقل در کنسول/فایل لاگ (Serilog) بماند
            logger.LogCritical(dbEx, "ثبت خطا در دیتابیس هم ناموفق بود. خطای اصلی: {Message}", exception.Message);
        }
    }
}
