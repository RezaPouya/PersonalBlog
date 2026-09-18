using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Auth;

public class LoginAdminCommand : ICommand<bool>
{
    [Required(ErrorMessage = "ایمیل اجباری است")]
    [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "رمز عبور اجباری است")]
    public string Password { get; set; } = default!;

    public bool RememberMe { get; set; }

    public string CaptchaId { get; set; } = default!;
    public string CaptchaAnswer { get; set; } = default!;
}