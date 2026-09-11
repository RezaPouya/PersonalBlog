using FluentValidation;
using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Validators;

public class CreateCommentInputDtoValidator : AbstractValidator<CreateCommentInputDto>
{
    public CreateCommentInputDtoValidator()
    {
        RuleFor(x => x.PostId).GreaterThan(0);
        RuleFor(x => x.DisplayName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Content).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.RecaptchaToken).NotEmpty();
    }
}
