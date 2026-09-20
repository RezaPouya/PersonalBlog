using AppServices.Commons;
using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.ContactMessages;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Site.ContactMessages;

public class CreateContactMessageCommandHandler(
    IValidator<CreateContactMessageCommand> validator,
    ICaptchaService captchaService,
    IContactMessageRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateContactMessageCommand, int>
{
    public async Task<int> Handle(CreateContactMessageCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        input.Sanitize();

        var isCaptchaValid = await captchaService.ValidateAsync(input.CaptchaId, input.CaptchaAnswer, cancellationToken);
        if (!isCaptchaValid)
            throw new BusinessException("پاسخ سؤال امنیتی درست نیست.");

        var message = new ContactMessage
        {
            FullName = input.FullName,
            Email = input.Email,
            Cellphone = input.Cellphone ?? "",
            Subject = input.Subject,
            Body = input.Body,
            IpAddress = input.IpAddress,
            IsRead = false
        };

        repository.Create(message);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return message.Id;
    }
}
