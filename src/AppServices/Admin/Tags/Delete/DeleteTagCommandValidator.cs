using FluentValidation;

namespace AppServices.Admin.Tags.Delete;

public class DeleteTagCommandValidator
    : AbstractValidator<DeleteTagCommand>
{
    public DeleteTagCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}