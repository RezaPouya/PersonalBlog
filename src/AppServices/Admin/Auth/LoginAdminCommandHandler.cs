using AppServices.Commons;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.Domain.Entities.Identities;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Auth;

public class LoginAdminCommandHandler(IValidator<LoginAdminCommand> validator,
    ICaptchaService captchaService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : ICommandHandler<LoginAdminCommand, bool>
{
    public async Task<bool> Handle(LoginAdminCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // اعتبارسنجی کپچا
        if (!await captchaService.ValidateAsync(input.CaptchaId, input.CaptchaAnswer, cancellationToken))
            throw new BusinessException("پاسخ کپچا صحیح نیست. لطفاً دوباره تلاش کنید.");

        var user = await userManager.FindByEmailAsync(input.Email);
        if (user is null)
            throw new BusinessException("ایمیل یا رمز عبور اشتباه است.");

        var result = await signInManager.PasswordSignInAsync(
            user,
            input.Password,
            input.RememberMe,
            lockoutOnFailure: true);

        if (result.IsLockedOut)
            throw new BusinessException("حساب کاربری به دلیل تلاش‌های ناموفق موقتاً قفل شده است.");

        if (!result.Succeeded)
            throw new BusinessException("ایمیل یا رمز عبور اشتباه است.");

        return true;
    }
}