global using Infrastructure.Persistance.SqlServer.CacheManagement;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using PersonalBlog.Domain.Entities.Identities;
using AppServices.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PersonalBlog.Domain.Commons;

namespace Infrastructure.Persistance.SqlServer;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DatabaseConnection")
            ?? throw new InvalidOperationException("Connection string 'DatabaseConnection' not found.");

        services.AddSingleton(new DbOptions
        {
            ConnectionString = connectionString,
        });


        services.TryAddTransient<AppDbContextSaveChangesInterceptor>();

        services.AddDbContext<AppDbContext>((sp, options) =>
            options.UseSqlServer(connectionString, b =>
            {
                b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName); // بهتر است از نوع DbContext استفاده شود
                b.CommandTimeout(60 * 5);
            })
            .AddInterceptors(sp.GetRequiredService<AppDbContextSaveChangesInterceptor>())
            .EnableDetailedErrors(false)); // کاملاً صحیح است

        services.AddIdentityCore<AppUser>(options =>
        {
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        })
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddMemoryCache();
        services.AddSingleton<ILocalCacheManager, LocalCacheManager>();
        services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}