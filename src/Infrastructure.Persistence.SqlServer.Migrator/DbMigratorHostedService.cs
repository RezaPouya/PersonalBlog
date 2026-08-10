using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence.SqlServer.Migrator;

public class DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime, IConfiguration configuration)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        ServiceCollection services = new ServiceCollection();

        Migrate_Core_Db(services);

        await Task.CompletedTask;

        hostApplicationLifetime.StopApplication();
    }

    private void Migrate_Core_Db(ServiceCollection services)
    {
        try
        {
            var connectionString = configuration.GetConnectionString("DatabaseConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString,
                        b =>
                        {
                            b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                            b.CommandTimeout(60 * 60);
                        })
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .EnableSensitiveDataLogging()
            );

            using (IServiceScope scope = services.BuildServiceProvider().CreateScope())
            {
                AppDbContext? dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                if (dbContext is not null)
                {
                    dbContext.Database.SetCommandTimeout(60 * 60); // 60 minutes
                    dbContext.Database.Migrate();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception:");
            Console.WriteLine(ex.ExceptionToString());
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}