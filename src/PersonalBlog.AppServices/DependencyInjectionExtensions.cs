using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalBlog.AppServices.Options;
using PersonalBlog.AppServices.Services.Blog;
using PersonalBlog.AppServices.Services.Blog.Imps;
using PersonalBlog.AppServices.Services.Common;
using PersonalBlog.AppServices.Services.Common.Imps;

namespace PersonalBlog.AppServices;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Options
        services.Configure<RecaptchaSettings>(configuration.GetSection("RecaptchaSettings"));
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<SocialLinksSettings>(configuration.GetSection("SocialLinks"));
        services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));

        // Current user
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentAppUser, CurrentAppUser>();

        // Common services
        services.AddHttpClient("recaptcha");
        services.AddScoped<ICaptchaService, RecaptchaService>();
        services.AddScoped<IVisitService, VisitService>();
        services.AddScoped<ISearchService, SearchService>();
        services.AddScoped<IExceptionLogService, ExceptionLogService>();
        services.AddScoped<IEmailService, EmailService>();

        // Blog services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IContactMessageService, ContactMessageService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();

        return services;
    }
}
