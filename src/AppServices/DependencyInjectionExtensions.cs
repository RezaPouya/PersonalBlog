using AppServices.Commons;
using AppServices.Commons.Imps;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalBlog.AppServices.Options;

namespace AppServices
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Options
            services.Configure<RecaptchaSettings>(configuration.GetSection("RecaptchaSettings"));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<SocialLinksSettings>(configuration.GetSection("SocialLinks"));
            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));

            services.AddTransient<IHtmlSanitizerService, HtmlSanitizerService>();
            services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();

            services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime() // WithTransientLifetime
            );

            // Context
            services.AddHttpContextAccessor();
            //services.AddScoped<ICurrentAppUser, CurrentAppUser>();

            // Common Services
            //services.AddHttpClient("recaptcha");
            //services.AddScoped<ICaptchaService, RecaptchaService>();
            //services.AddScoped<IVisitService, VisitService>();
            //services.AddScoped<ISearchService, SearchService>();
            //services.AddScoped<IExceptionLogService, ExceptionLogService>();
            //services.AddScoped<IEmailService, EmailService>();

            // Blog Services
            //services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped<ITagService, TagService>();
            //services.AddScoped<IPostService, PostService>();
            //services.AddScoped<ICommentService, CommentService>();
            //services.AddScoped<ICourseService, CourseService>();
            //services.AddScoped<IProjectService, ProjectService>();
            //services.AddScoped<IContactMessageService, ContactMessageService>();
            //services.AddScoped<ISubscriptionService, SubscriptionService>();

            // Validators
            //services.AddValidatorsFromAssemblyContaining<DependencyInjectionExtensions>();

            return services;
        }
    }
}
