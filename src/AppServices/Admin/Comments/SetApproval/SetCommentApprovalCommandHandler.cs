using FluentValidation;
using PersonalBlog.Domain.Entities.Comments;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Comments.SetApproval;

public class SetCommentApprovalCommandHandler(
    IValidator<SetCommentApprovalCommand> validator,
    ICommentRepository commentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<SetCommentApprovalCommand, int>
{
    public async Task<int> Handle(SetCommentApprovalCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var comment = await commentRepository.FindByIdAsync(input.Id, cancellationToken);
        if (comment is null)
            throw new BusinessException("کامنت با این شناسه یافت نشد.");

        comment.IsApproved = input.IsApproved;
        if (input.IsApproved)
            comment.IsSpam = false;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }
}
