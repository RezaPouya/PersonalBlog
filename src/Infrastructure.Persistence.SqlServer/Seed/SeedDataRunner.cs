global using Microsoft.AspNetCore.Identity;
global using Microsoft.Extensions.Logging;
global using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Entities.Posts;

namespace Infrastructure.Persistence.SqlServer.Seed;

/// <summary>
/// دیتای اولیه: نقش و کاربر Admin (از appsettings خوانده می‌شود)، یک دسته‌بندی،
/// یک دوره و یک پست نمونه - طبق درخواست پرامپت اصلی (بخش ۶).
/// </summary>
public static class SeedDataRunner
{
    public static async Task RunAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("SeedData");

        try
        {
            // نقش Admin
            if (!await roleManager.RoleExistsAsync(AppRoleConstants.Admin))
                await roleManager.CreateAsync(new AppRole(AppRoleConstants.Admin));

            // کاربر Admin (از appsettings.json -> AdminSeed)
            var adminEmail = configuration["AdminSeed:Email"] ?? "admin@example.com";
            var adminPassword = configuration["AdminSeed:Password"] ?? "Admin@123456";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser is null)
            {
                adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    DisplayName = "مدیر سایت",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, AppRoleConstants.Admin);
                else
                    logger.LogWarning("ساخت کاربر ادمین ناموفق بود: {Errors}",
                        string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // دسته‌بندی نمونه
            if (!dbContext.Categories.Any())
            {
                var category = new Category
                {
                    CreatedAt = new DateTime(2026, 12, 9),
                    UpdatedAt = new DateTime(2026, 12, 9),
                    Title = "برنامه‌نویسی",
                    Slug = "development",
                    Description = "یادداشت‌ها و آموزش‌های برنامه‌نویسی",
                    TinyUrl = "ct_development"
                };
                dbContext.Categories.Add(category);

                var category2 = new Category
                {
                    CreatedAt = new DateTime(2026, 12, 9),
                    UpdatedAt = new DateTime(2026, 12, 9),
                    Title = "معماری نرم افزار",
                    Slug = "software-architecture",
                    Description = "معماری نرم افزار",
                    TinyUrl = "ct_software_architecture"
                };

                dbContext.Categories.Add(category2);

                var category3 = new Category
                {
                    CreatedAt = new DateTime(2026, 12, 9),
                    UpdatedAt = new DateTime(2026, 12, 9),
                    Title = "توسعه شخصی",
                    Slug = "self-development",
                    Description = "نوشته های توسعه شخصی",
                    TinyUrl = "ct_self_architecture"
                };

                dbContext.Categories.Add(category3);
                await dbContext.SaveChangesAsync();

                var course = new Course
                {
                    CreatedAt = new DateTime(2026, 12, 9),
                    UpdatedAt = new DateTime(2026, 12, 9),
                    Title = "قضاوت مهندسی",
                    Slug = "engineering-judgment",
                    Description = "قضاوت مهندسی",
                    IsPublished = false,
                    TinyUrl = "ct_engineering_judgment"
                };
                dbContext.Courses.Add(course);

                await dbContext.SaveChangesAsync();

                dbContext.Posts.Add(new Post
                {
                    Title = "سلام دنیا! اولین پست وبلاگ",
                    Slug = "hello-world",
                    Summary = "اولین پست این وبلاگ شخصی، برای تست راه‌اندازی اولیه.",
                    Content = "<p>این یک پست نمونه است که هنگام seed دیتابیس ساخته می‌شود.</p>",
                    IsPublished = true,
                    CreatedAt = new DateTime(2026, 12, 9),
                    UpdatedAt = new DateTime(2026, 12, 9),
                    PublishedAt = new DateTime(2026, 12, 9),
                    CategoryId = category.Id,
                });
                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "خطا در اجرای Seed دیتای اولیه");
        }
    }
}
