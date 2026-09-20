using FluentValidation;

namespace AppServices.Admin.Comments.Delete;

public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه اجباری است.");
    }
}
