using FluentValidation;
using PersonalBlog.Domain.Entities.ContactMessages;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.ContactMessages.Delete;

public class DeleteContactMessageCommandHandler(
    IValidator<DeleteContactMessageCommand> validator,
    IContactMessageRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteContactMessageCommand, int>
{
    public async Task<int> Handle(DeleteContactMessageCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var message = await repository.FindByIdAsync(input.Id, cancellationToken);
        if (message is null)
            throw new BusinessException("پیام با این شناسه یافت نشد.");

        repository.Delete(message);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return message.Id;
    }
}
