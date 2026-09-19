using FluentValidation;
using PersonalBlog.Domain.Entities.Subscriptions;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Subscriptions.Delete;

public class DeleteSubscriptionCommandHandler(
    IValidator<DeleteSubscriptionCommand> validator,
    ISubscriptionRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteSubscriptionCommand, int>
{
    public async Task<int> Handle(DeleteSubscriptionCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var subscription = await repository.FindByIdAsync(input.Id, cancellationToken);
        if (subscription is null)
            throw new BusinessException("مشترک با این شناسه یافت نشد.");

        repository.Delete(subscription);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return subscription.Id;
    }
}
