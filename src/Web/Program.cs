using AppServices;
using Infrastructure.Persistence.SqlServer;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Identities;
using Web.Components;

var builder = WebApplication.CreateBuilder(args);

// 1. Razor Components & Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 2. Application Layer (CQRS Handlers, Validators, etc.)
builder.Services.AddApplicationServices(builder.Configuration);

// 3. Infrastructure Layer (DbContext, Repositories, Cache)
// ⚠️ IMPORTANT: Remove AddIdentityCore from AddInfrastructurePersistenceSqlServer!
builder.Services.AddInfrastructurePersistenceSqlServer(builder.Configuration);

// 4. ASP.NET Core Identity (Registered in Web layer, NOT Infrastructure)
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// 5. Authentication & Cookie Configuration
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/admin/login";
    options.AccessDeniedPath = "/admin/login";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
    options.Cookie.Name = "PersonalBlog.Auth";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

// 6. Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole(AppRoleConstants.Admin));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseStaticFiles();

// ⚠️ CRITICAL: Correct Middleware Order for Blazor Server + Identity
app.UseRouting();

app.UseAuthentication();    // Must come BEFORE Authorization
app.UseAuthorization();     // Must come BEFORE Antiforgery

app.UseAntiforgery();       // Must come AFTER Authentication/Authorization for Blazor

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();