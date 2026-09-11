using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Web.Components.Admin.Auth;

/// <summary>
/// چون بلزور سرور نمی‌تواند مستقیماً از کوکی احراز هویت بخواند،
/// از طریق IHttpContextAccessor وضعیت کاربر لاگین‌شده را دریافت می‌کنیم.
/// </summary>
public class AdminAuthStateProvider(IHttpContextAccessor httpContextAccessor) : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var claims = httpContext.User.Claims.ToList();

            // اگر نقش در Claims نیست، از UserManager استفاده نمی‌کنیم
            // چون فقط یک نقش داریم، مستقیم چک می‌کنیم
            var identity = new ClaimsIdentity(claims, "AdminAuth");
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
    }

    /// <summary>
    /// فراخوانی بعد از لاگین/لاگوت برای به‌روزرسانی وضعیت
    /// </summary>
    public void NotifyUserChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}