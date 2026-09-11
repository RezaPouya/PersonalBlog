using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PersonalBlog.Domain.Constants;

namespace PersonalBlog.AppServices;

public class CurrentAppUser : ICurrentAppUser
{
    public long UserId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public bool IsAuthenticated { get; private set; }
    public string IpAddress { get; private set; } = string.Empty;
    public string? UserAgent { get; private set; }

    private readonly List<string> _roles = new();

    public CurrentAppUser(IHttpContextAccessor httpContextAccessor)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null)
            return;

        IpAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        UserAgent = httpContext.Request.Headers.UserAgent.ToString();

        var user = httpContext.User;
        if (user?.Identity?.IsAuthenticated != true)
            return;

        IsAuthenticated = true;
        UserName = user.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

        var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (long.TryParse(idClaim, out var id))
            UserId = id;

        _roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
    }

    public bool IsAdmin() => _roles.Contains(AppRoleConstants.Admin);
}
