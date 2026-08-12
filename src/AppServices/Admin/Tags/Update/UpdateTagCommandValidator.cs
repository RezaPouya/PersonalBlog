using FluentValidation;

namespace AppServices.Admin.Tags.Update;

public class UpdateTagCommandValidator
    : AbstractValidator<UpdateTagCommand>
{
    public UpdateTagCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);
    }
}