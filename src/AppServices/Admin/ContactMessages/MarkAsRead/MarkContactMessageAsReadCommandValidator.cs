using FluentValidation;

namespace AppServices.Admin.ContactMessages.MarkAsRead;

public class MarkContactMessageAsReadCommandValidator : AbstractValidator<MarkContactMessageAsReadCommand>
{
    public MarkContactMessageAsReadCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه اجباری است.");
    }
}
