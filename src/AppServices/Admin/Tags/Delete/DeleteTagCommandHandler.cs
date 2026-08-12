using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Tags;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Tags.Delete;

public class DeleteTagCommandHandler(
    IValidator<DeleteTagCommand> validator,
    ILocalCacheManager localCacheManager,
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteTagCommand, int>
{
    public async Task<int> Invoke(
        DeleteTagCommand input,
        CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var tag = await tagRepository.FindByIdAsync(
            input.Id,
            cancellationToken);

        if (tag is null)
            throw new BusinessException("برچسب یافت نشد.");

        if (await tagRepository.HasAnyPostAsync(
                tag.Id,
                cancellationToken))
        {
            throw new BusinessException(
                "این برچسب در یک یا چند مطلب استفاده شده و قابل حذف نیست.");
        }

        tagRepository.Delete(tag);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        localCacheManager.Remove(CacheKeys.TagsList);

        return tag.Id;
    }
}