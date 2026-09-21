using FluentValidation;

namespace AppServices.Site.Comments;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.PostId).GreaterThan(0).WithMessage("پست نامعتبر است.");
        RuleFor(x => x.DisplayName).NotEmpty().WithMessage("نام اجباری است.").MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().WithMessage("ایمیل اجباری است.").EmailAddress().WithMessage("ایمیل معتبر نیست.");
        RuleFor(x => x.Content).NotEmpty().WithMessage("متن کامنت نباید خالی باشد.").MaximumLength(2000);
        RuleFor(x => x.CaptchaId).NotEmpty().WithMessage("لطفاً صفحه را رفرش کنید.");
        RuleFor(x => x.CaptchaAnswer).NotEmpty().WithMessage("پاسخ سؤال امنیتی را وارد کنید.");
    }
}
