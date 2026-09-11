namespace PersonalBlog.AppServices.Services.Common;

public interface ICaptchaService
{
    Task<bool> ValidateAsync(string token, CancellationToken cancellationToken = default);
}
