using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Tags;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Tags.Update;

public class UpdateTagCommandHandler(
    IValidator<UpdateTagCommand> validator,
    ILocalCacheManager localCacheManager,
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateTagCommand, int>
{
    public async Task<int> Invoke(
        UpdateTagCommand input,
        CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        input.Sanitize();

        var tag = await tagRepository.FindByIdAsync(
            input.Id,
            cancellationToken);

        if (tag is null)
            throw new BusinessException("برچسب یافت نشد.");

        if (await tagRepository.IsExistsByTitleAsync(
                input.Title,
                input.Id,
                cancellationToken))
        {
            throw new BusinessException(
                "برچسب دیگری با این عنوان وجود دارد.");
        }

        tag.Title = input.Title;

        tagRepository.SetUpdatedAt(tag);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        localCacheManager.Remove(CacheKeys.TagsList);

        return tag.Id;
    }
}