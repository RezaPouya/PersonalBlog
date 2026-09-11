using FluentValidation;

namespace AppServices.Admin.Auth;

public class LoginAdminCommandValidator : AbstractValidator<LoginAdminCommand>
{
    public LoginAdminCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.CaptchaId).NotEmpty();
        RuleFor(x => x.CaptchaAnswer).NotEmpty();
    }
}