using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Commons.Base;
using PersonalBlog.Domain.Entities.Identities;
using PersonalBlog.Infrastructure.CacheManagement;
using PersonalBlog.Infrastructure.Persistences;

namespace PersonalBlog.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DatabaseConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString, sql =>
            {
                sql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                sql.CommandTimeout(60 * 5);
            }));

        // Identity (فقط برای Admin سایت - بدون ثبت‌نام عمومی)
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
