using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Subscriptions;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Site.Subscriptions;

public class CreateSubscriptionCommandHandler(
    IValidator<CreateSubscriptionCommand> validator,
    ISubscriptionRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateSubscriptionCommand, int>
{
    public async Task<int> Handle(CreateSubscriptionCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        input.Sanitize();

        var alreadyActive = await repository.IsExistsByEmailAsync(input.Email, cancellationToken);
        if (alreadyActive)
            throw new BusinessException("این ایمیل قبلاً در خبرنامه عضو شده است.");

        var subscription = new Subscription
        {
            Email = input.Email,
            IsActive = true
        };

        repository.Create(subscription);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return subscription.Id;
    }
}
