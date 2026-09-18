using AppServices.Admin.Posts.Delete;
using AppServices.Commons;
using AppServices.Options;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<ICaptchaService, MathCaptchaService>();
            services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();

            services.Scan(scan => scan
                .FromAssemblies(
                    typeof(DeletePostCommandHandler).Assembly,   // AppServices
                    typeof(CreateCategoryCommandValidator).Assembly,
                    typeof(DeletePostCommand).Assembly

                // add other assemblies you actually want scanned
                )
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // Context
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
