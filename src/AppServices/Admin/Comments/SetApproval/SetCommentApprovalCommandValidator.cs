using FluentValidation;

namespace AppServices.Admin.Comments.SetApproval;

public class SetCommentApprovalCommandValidator : AbstractValidator<SetCommentApprovalCommand>
{
    public SetCommentApprovalCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه اجباری است.");
    }
}
