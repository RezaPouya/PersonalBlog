using FluentValidation;

namespace AppServices.Admin.ContactMessages.Delete;

public class DeleteContactMessageCommandValidator : AbstractValidator<DeleteContactMessageCommand>
{
    public DeleteContactMessageCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه اجباری است.");
    }
}
