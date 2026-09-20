using FluentValidation;
using PersonalBlog.Domain.Entities.Comments;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Comments.Delete;

public class DeleteCommentCommandHandler(
    IValidator<DeleteCommentCommand> validator,
    ICommentRepository commentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteCommentCommand, int>
{
    public async Task<int> Handle(DeleteCommentCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var comment = await commentRepository.FindByIdAsync(input.Id, cancellationToken);
        if (comment is null)
            throw new BusinessException("کامنت با این شناسه یافت نشد.");

        commentRepository.Delete(comment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }
}
