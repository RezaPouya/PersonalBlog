using FluentValidation;
using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Validators;

public class CreateContactMessageInputDtoValidator : AbstractValidator<CreateContactMessageInputDto>
{
    public CreateContactMessageInputDtoValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Subject).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Body).NotEmpty().MaximumLength(4000);
        RuleFor(x => x.RecaptchaToken).NotEmpty();
    }
}
