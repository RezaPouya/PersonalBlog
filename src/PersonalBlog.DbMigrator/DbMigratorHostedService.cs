using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonalBlog.Infrastructure;
using PersonalBlog.Infrastructure.Seed;

namespace PersonalBlog.DbMigrator;

/// <summary>
/// عیناً هم‌الگو با DbMigratorHostedService پروژه‌ی نمونه: یک پروژه‌ی کنسول مجزا
/// که مایگریشن‌ها را اجرا و دیتای اولیه را seed می‌کند.
/// اجرا: dotnet run --project src/PersonalBlog.DbMigrator
/// </summary>
public class DbMigratorHostedService(
    IHostApplicationLifetime lifetime,
    IServiceProvider serviceProvider,
    ILogger<DbMigratorHostedService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            logger.LogInformation("در حال اجرای Migration...");
            await dbContext.Database.MigrateAsync(cancellationToken);
            logger.LogInformation("Migration با موفقیت اجرا شد.");

            logger.LogInformation("در حال seed کردن دیتای اولیه...");
            await SeedDataRunner.RunAsync(serviceProvider);
            logger.LogInformation("Seed با موفقیت انجام شد.");
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "اجرای DbMigrator ناموفق بود.");
        }
        finally
        {
            lifetime.StopApplication();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
