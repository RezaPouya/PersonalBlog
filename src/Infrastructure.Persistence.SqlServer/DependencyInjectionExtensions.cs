global using Infrastructure.Persistence.SqlServer.CacheManagement;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using PersonalBlog.Domain.Entities.Identities;
using AppServices.Options;
using PersonalBlog.Domain.Commons;

namespace Infrastructure.Persistence.SqlServer;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructurePersistenceSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DatabaseConnection")
            ?? throw new InvalidOperationException("Connection string 'DatabaseConnection' not found.");

        services.AddSingleton(new DbOptions
        {
            ConnectionString = connectionString,
        });

        services.AddScoped<AppDbContextSaveChangesInterceptor>();

        services.AddDbContext<AppDbContext>((sp, options) =>
            options.UseSqlServer(connectionString, b =>
            {
                b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName); // بهتر است از نوع DbContext استفاده شود
                b.CommandTimeout(60 * 5);
            })
            .AddInterceptors(sp.GetRequiredService<AppDbContextSaveChangesInterceptor>())
            .EnableDetailedErrors(false)); // کاملاً صحیح است

        services.AddMemoryCache();
        services.AddSingleton<ILocalCacheManager, LocalCacheManager>();

        services.Scan(scan => scan
         .FromAssembliesOf(typeof(AppDbContext))
         .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
         .AsImplementedInterfaces()
         .WithScopedLifetime());

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}