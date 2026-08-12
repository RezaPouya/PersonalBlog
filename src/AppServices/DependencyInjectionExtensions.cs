using AppServices.Commons;
using AppServices.Commons.Imps;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppServices
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IHtmlSanitizerService, HtmlSanitizerService>();
            services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();

            services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime() // WithTransientLifetime
            );


            return services;
        }
    }
}
