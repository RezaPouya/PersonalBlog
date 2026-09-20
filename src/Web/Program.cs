using AppServices;
using AppServices.Admin.Auth;
using AppServices.Commons;
using AppServices.Options;
using Infrastructure.Persistence.SqlServer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;                 // ← for [FromForm] / [FromServices]
using Microsoft.Extensions.Options;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Entities.Identities;
using PersonalBlog.Domain.Entities.Posts;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Web.Components;
using Web.Components.Admin.Auth;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

// HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// لایه‌ها
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructurePersistenceSqlServer(builder.Configuration);

// Identity + Authentication
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Authentication State Provider برای بلزور
builder.Services.AddScoped<AdminAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<AdminAuthStateProvider>());

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/admin/login";
    options.LogoutPath = "/admin/logout";
    options.AccessDeniedPath = "/admin/login";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
    options.Cookie.Name = "PersonalBlog.Auth";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole(AppRoleConstants.Admin));
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMemoryCache();

// آپلود/دانلود فایل (تصاویر پست‌ها و ...)
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

// ==================== Admin Auth Endpoints ====================

// ورود ادمین (فرم پست سنتی برای تنظیم کوکی)
app.MapPost("/admin/login", async (
    [FromForm] LoginAdminCommand model,
    [FromServices] LoginAdminCommandHandler handler,
    HttpContext httpContext) =>
{
    try
    {
        await handler.Handle(model, httpContext.RequestAborted);
        return Results.Redirect("/admin");
    }
    catch (Exception ex)
    {
        return Results.Redirect($"/admin/login?error={Uri.EscapeDataString(ex.Message)}");
    }
})
.DisableAntiforgery(); // فرم پست سنتی بدون توکن ضد جعل

// خروج ادمین
app.MapPost("/admin/logout", async (
    SignInManager<AppUser> signInManager,
    HttpContext httpContext) =>
{
    await signInManager.SignOutAsync();
    return Results.Redirect("/admin/login");
})
.RequireAuthorization("AdminOnly");

// ==================== دانلود فایل آپلودی (فقط ادمین) ====================
// نکته: نمایش عمومی تصاویر در سایت (مثلاً <img src="/uploads/posts/xxx.jpg">)
// نیازی به این اندپوینت ندارد و همان app.UseStaticFiles() بالا آن را سرو می‌کند.
// این اندپوینت برای دانلود اجباری (Content-Disposition: attachment) از پنل ادمین است.
app.MapGet("/admin/files/download", (string path, IWebHostEnvironment env) =>
{
    if (string.IsNullOrWhiteSpace(path) || !path.StartsWith("/uploads/", StringComparison.OrdinalIgnoreCase))
        return Results.BadRequest("مسیر نامعتبر است.");

    var relativePath = path.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
    var fullPath = Path.GetFullPath(Path.Combine(env.WebRootPath, relativePath));
    var uploadsRoot = Path.GetFullPath(Path.Combine(env.WebRootPath, "uploads"));

    // دفاع در برابر Path Traversal (../..)
    if (!fullPath.StartsWith(uploadsRoot, StringComparison.OrdinalIgnoreCase) || !File.Exists(fullPath))
        return Results.NotFound();

    var fileName = Path.GetFileName(fullPath);
    return Results.File(fullPath, "application/octet-stream", fileName);
})
.RequireAuthorization("AdminOnly");

// =================================================================

// ==================== SEO: robots.txt / sitemap.xml / rss.xml ====================

app.MapGet("/robots.txt", (HttpRequest request) =>
{
    var baseUrl = $"{request.Scheme}://{request.Host}";
    var content = "User-agent: *\n" +
                  "Allow: /\n" +
                  "Disallow: /admin/\n\n" +
                  $"Sitemap: {baseUrl}/sitemap.xml\n";

    return Results.Text(content, "text/plain", Encoding.UTF8);
});

app.MapGet("/sitemap.xml", async (
    HttpRequest request,
    IPostRepository postRepository,
    ICategoryRepository categoryRepository,
    ICourseRepository courseRepository,
    CancellationToken cancellationToken) =>
{
    var baseUrl = $"{request.Scheme}://{request.Host}";

    var urls = new List<(string Loc, DateTime? LastMod)>
    {
        (baseUrl + "/", null),
        (baseUrl + "/posts", null),
        (baseUrl + "/courses", null),
        (baseUrl + "/projects", null),
        (baseUrl + "/about", null),
        (baseUrl + "/contact", null),
    };

    var posts = await postRepository.GetAllPublishedForSitemapAsync(cancellationToken);
    urls.AddRange(posts.Select(p => (baseUrl + $"/post/{p.Slug}", (DateTime?)p.UpdatedAt)));

    var categories = await categoryRepository.GetAllForSitemapAsync(cancellationToken);
    urls.AddRange(categories.Select(c => (baseUrl + $"/category/{c.Slug}", (DateTime?)c.UpdatedAt)));

    var courses = await courseRepository.GetAllPublishedForSitemapAsync(cancellationToken);
    urls.AddRange(courses.Select(c => (baseUrl + $"/course/{c.Slug}", (DateTime?)c.UpdatedAt)));

    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
    var doc = new XDocument(
        new XDeclaration("1.0", "utf-8", null),
        new XElement(ns + "urlset",
            urls.Select(u =>
                new XElement(ns + "url",
                    new XElement(ns + "loc", u.Loc),
                    u.LastMod.HasValue
                        ? new XElement(ns + "lastmod", u.LastMod.Value.ToString("yyyy-MM-dd"))
                        : null))));

    using var ms = new MemoryStream();
    using (var writer = XmlWriter.Create(ms, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
    {
        doc.Save(writer);
    }

    return Results.File(ms.ToArray(), "application/xml");
});

app.MapGet("/rss.xml", async (
    HttpRequest request,
    IPostRepository postRepository,
    IOptions<SiteSettings> siteSettingsOptions,
    CancellationToken cancellationToken) =>
{
    var baseUrl = $"{request.Scheme}://{request.Host}";
    var siteSettings = siteSettingsOptions.Value;

    var posts = await postRepository.GetLatestForFeedAsync(30, cancellationToken);

    var channel = new XElement("channel",
        new XElement("title", siteSettings.Title),
        new XElement("link", baseUrl),
        new XElement("description", siteSettings.Description),
        new XElement("language", "fa-ir"),
        posts.Select(p => new XElement("item",
            new XElement("title", p.Title),
            new XElement("link", $"{baseUrl}/post/{p.Slug}"),
            new XElement("guid", $"{baseUrl}/post/{p.Slug}"),
            new XElement("description", p.Summary),
            new XElement("pubDate", (p.PublishedAt ?? DateTime.UtcNow).ToUniversalTime().ToString("R")))));

    var doc = new XDocument(
        new XDeclaration("1.0", "utf-8", null),
        new XElement("rss", new XAttribute("version", "2.0"), channel));

    using var ms = new MemoryStream();
    using (var writer = XmlWriter.Create(ms, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
    {
        doc.Save(writer);
    }

    return Results.File(ms.ToArray(), "application/rss+xml");
});

// =================================================================

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();