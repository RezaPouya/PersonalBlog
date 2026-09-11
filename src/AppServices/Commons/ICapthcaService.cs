namespace AppServices.Commons;

public interface ICaptchaService
{
    Task<(string CaptchaId, string Question)> GenerateChallengeAsync(CancellationToken cancellationToken);
    Task<bool> ValidateAsync(string captchaId, string answer, CancellationToken cancellationToken);
}
