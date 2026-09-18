using AppServices;
using AppServices.Admin.Auth;
using Infrastructure.Persistence.SqlServer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Identities;
using Web.Components;
using Web.Components.Admin.Auth;

var builder = WebApplication.CreateBuilder(args);

// این خط را قبل از ثبت‌های دیگر داشته باشید
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

builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<AdminAuthStateProvider>());


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

builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

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
    [Microsoft.AspNetCore.Mvc.FromForm] LoginAdminCommand model,
    LoginAdminCommandHandler handler,
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
app.MapPost("/admin/logout", async (SignInManager<AppUser> signInManager, HttpContext httpContext) =>
{
    await signInManager.SignOutAsync();
    return Results.Redirect("/admin/login");
})
.RequireAuthorization("AdminOnly");

// =================================================================

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();