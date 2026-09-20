using FluentValidation;

namespace AppServices.Site.Subscriptions;

public class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("ایمیل اجباری است.").EmailAddress().WithMessage("ایمیل معتبر نیست.");
    }
}
