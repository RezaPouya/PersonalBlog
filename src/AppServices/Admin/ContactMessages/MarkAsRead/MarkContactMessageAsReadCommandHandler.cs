using FluentValidation;
using PersonalBlog.Domain.Entities.ContactMessages;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.ContactMessages.MarkAsRead;

public class MarkContactMessageAsReadCommandHandler(
    IValidator<MarkContactMessageAsReadCommand> validator,
    IContactMessageRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<MarkContactMessageAsReadCommand, int>
{
    public async Task<int> Handle(MarkContactMessageAsReadCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var message = await repository.FindByIdAsync(input.Id, cancellationToken);
        if (message is null)
            throw new BusinessException("پیام با این شناسه یافت نشد.");

        message.IsRead = true;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return message.Id;
    }
}
