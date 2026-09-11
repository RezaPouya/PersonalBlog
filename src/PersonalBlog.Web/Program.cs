using Microsoft.AspNetCore.Identity;
using PersonalBlog.AppServices;
using PersonalBlog.AppServices.Services.Common;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Identities;
using PersonalBlog.Infrastructure;
using PersonalBlog.Web.Components;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------- Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

// ---------------------------------------------------------------- Layers
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAppServices(builder.Configuration);

// ---------------------------------------------------------------- Identity / Auth (فقط Admin)
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
        options.LoginPath = "/admin/login";
        options.AccessDeniedPath = "/admin/login";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole(AppRoleConstants.Admin));

builder.Services.AddCascadingAuthenticationState();

// ---------------------------------------------------------------- Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// SignalR: تنظیم مناسب برای سرور با منابع محدود (طبق بخش ۸ اسپک)
builder.Services.AddServerSideBlazor(options =>
{
    options.DisconnectedCircuitMaxRetained = 20;
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(2);
});

builder.Services.AddResponseCompression(); // Gzip

var app = builder.Build();

// ---------------------------------------------------------------- Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// میان‌افزار سراسری ثبت خطا در دیتابیس (طبق درخواست کاربر - بدون Monitoring جدا)
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var exceptionLogService = context.RequestServices.GetRequiredService<IExceptionLogService>();
        await exceptionLogService.LogAsync(
            ex,
            requestPath: context.Request.Path,
            ipAddress: context.Connection.RemoteIpAddress?.ToString(),
            userAgent: context.Request.Headers.UserAgent.ToString());
        throw;
    }
});

app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// -------------------------------------------------------------------------
// Endpointهای ورود/خروج Admin با Minimal API (چون SignInAsync/SignOutAsync
// باید قبل از commit شدن پاسخ اجرا شوند و از داخل کامپوننت تعاملی Blazor Server
// قابل انجام نیستند - همان الگوی رسمی قالب Identity در Blazor Server).
// -------------------------------------------------------------------------
app.MapPost("/admin/login", async (HttpContext httpContext, SignInManager<AppUser> signInManager,
    string email, string password, string? returnUrl) =>
{
    var result = await signInManager.PasswordSignInAsync(email, password, isPersistent: true, lockoutOnFailure: true);

    if (result.Succeeded)
    {
        return Results.LocalRedirect(string.IsNullOrWhiteSpace(returnUrl) ? "/admin" : returnUrl);
    }

    return Results.LocalRedirect($"/admin/login?error=1");
});

app.MapPost("/admin/logout", async (SignInManager<AppUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.LocalRedirect("/");
});

// نقشه‌ی سایت پویا (SEO - بخش ۹ اسپک)
app.MapGet("/sitemap.xml", async (IPostService postService, HttpContext context) =>
{
    var (posts, _) = await postService.GetListAsync(new PersonalBlog.AppServices.Dtos.PostListFilterDto
    {
        Page = 1,
        PageSize = 1000
    });

    var baseUrl = $"{context.Request.Scheme}://{context.Request.Host}";
    var urls = string.Join("\n", posts.Select(p =>
        $"  <url><loc>{baseUrl}/post/{p.Slug}</loc></url>"));

    var xml = $"""
        <?xml version="1.0" encoding="UTF-8"?>
        <urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
          <url><loc>{baseUrl}/</loc></url>
        {urls}
        </urlset>
        """;

    return Results.Content(xml, "application/xml");
});

app.Run();
