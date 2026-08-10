using AppServices.Base;
using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities;
using PersonalBlog.Domain.Entities.Tags;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Tags.Create;

public class CreateTagCommandHandler(
    IValidator<CreateTagCommand> validator,
    ILocalCacheManager localCacheManager,
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateTagCommand, int>
{
    public async Task<int> Invoke(CreateTagCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        input.Sanitize();

        if (await tagRepository.IsExistsByTitleAsync(input.Title, cancellationToken))
            throw new BusinessException("برچسب با همین عنوان وجود دارد.");

        if (await tagRepository.IsExistsBySlugAsync(input.Slug, cancellationToken))
            throw new BusinessException("برچسب با همین اسلاگ وجود دارد.");

        var tag = new Tag
        {
            Title = input.Title,
            Slug = input.Slug
        };

        tagRepository.Create(tag);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        localCacheManager.Remove(CacheKeys.TagsList);

        return tag.Id;
    }
}
