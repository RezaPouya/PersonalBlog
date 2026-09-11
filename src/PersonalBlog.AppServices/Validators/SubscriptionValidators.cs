using FluentValidation;
using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Validators;

public class CreateSubscriptionInputDtoValidator : AbstractValidator<CreateSubscriptionInputDto>
{
    public CreateSubscriptionInputDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
