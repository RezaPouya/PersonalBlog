using FluentValidation;

namespace AppServices.Site.ContactMessages;

public class CreateContactMessageCommandValidator : AbstractValidator<CreateContactMessageCommand>
{
    public CreateContactMessageCommandValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("نام اجباری است.").MaximumLength(150);
        RuleFor(x => x.Email).NotEmpty().WithMessage("ایمیل اجباری است.").EmailAddress().WithMessage("ایمیل معتبر نیست.");
        RuleFor(x => x.Subject).NotEmpty().WithMessage("موضوع اجباری است.").MaximumLength(200);
        RuleFor(x => x.Body).NotEmpty().WithMessage("متن پیام نباید خالی باشد.").MaximumLength(3000);
        RuleFor(x => x.CaptchaId).NotEmpty().WithMessage("لطفاً صفحه را رفرش کنید.");
        RuleFor(x => x.CaptchaAnswer).NotEmpty().WithMessage("پاسخ سؤال امنیتی را وارد کنید.");
    }
}
