using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Tags;

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

        var currentTag = await tagRepository.GetByTitleAsync(input.Title, cancellationToken);

        if (currentTag is not null)
            return currentTag.Id;

        var tag = new Tag
        {
            Title = input.Title,
        };

        tagRepository.Create(tag);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        localCacheManager.Remove(CacheKeys.TagsList);

        return tag.Id;
    }
}
