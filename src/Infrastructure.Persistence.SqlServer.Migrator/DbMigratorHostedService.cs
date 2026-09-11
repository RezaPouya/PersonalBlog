using Infrastructure.Persistence.SqlServer.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonalBlog.Domain.Entities.Identities;

namespace Infrastructure.Persistence.SqlServer.Migrator;

public class DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime, IConfiguration configuration)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        ServiceCollection services = new ServiceCollection();
        await Migrate_Core_Db(services);
        await Task.CompletedTask;
        hostApplicationLifetime.StopApplication();
    }

    private async Task Migrate_Core_Db(ServiceCollection services)
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


            services.AddIdentityCore<AppUser>()
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddLogging(builder => builder.AddConsole());

            // IConfiguration برای SeedDataRunner
            services.AddSingleton(configuration);

            using IServiceScope scope = services.BuildServiceProvider().CreateScope();
            AppDbContext? dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            if (dbContext is not null)
            {
                dbContext.Database.SetCommandTimeout(60 * 60);
                dbContext.Database.Migrate();
                Console.WriteLine(" Migration is done");
            }

            //  اجرای Seed دیتای اولیه
            Console.WriteLine("Start Seeding ... ");
            await SeedDataRunner.RunAsync(scope.ServiceProvider);
            Console.WriteLine("The seed operation is complete.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: ");
            Console.WriteLine(ex.ExceptionToString());
        }

        Console.ReadKey();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}