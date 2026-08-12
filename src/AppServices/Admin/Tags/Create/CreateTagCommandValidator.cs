using FluentValidation;

namespace AppServices.Admin.Tags.Create;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
    }
}
