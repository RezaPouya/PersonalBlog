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
            // Context
            services.AddHttpContextAccessor();

            // Options
            services.Configure<RecaptchaSettings>(configuration.GetSection("RecaptchaSettings"));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<SocialLinksSettings>(configuration.GetSection("SocialLinks"));
            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));

            services.AddTransient<IHtmlSanitizerService, HtmlSanitizerService>();
            services.AddScoped<ICaptchaService, MathCaptchaService>();
            services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();

            //services.Scan(scan => scan
            //    .FromAssemblies(
            //        typeof(DeletePostCommandHandler).Assembly,   // AppServices
            //        typeof(CreateCategoryCommandValidator).Assembly,
            //        typeof(DeletePostCommand).Assembly

            //    // add other assemblies you actually want scanned
            //    )
            //    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
            //    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime());


            services.AddCommandsAndQueries();


            return services;
        }

        private static void AddCommandsAndQueries(this IServiceCollection services)
        {
            services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjectionExtensions).Assembly)

                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                // ۲. ثبت هندلرهای Command که مقداری برنمی‌گردانند (ICommandHandler<TCommand>)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                // ۳. ثبت هندلرهای Query (IQueryHandler<TQuery, TResult>)
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();
        }
    }
}
