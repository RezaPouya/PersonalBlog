using AppServices.Base;
using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Posts.Delete;

public class DeletePostCommandHandler(
    IValidator<DeletePostCommand> validator,
    ILocalCacheManager localCacheManager,
    IPostRepository postRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeletePostCommand, int>
{
    public async Task<int> Invoke(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var post = await postRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new BusinessException("پست یافت نشد.");

        post.IsDeleted = true;
        post.DeletedAt = DateTime.Now;
        postRepository.SetUpdatedAt(post);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        localCacheManager.Remove(CacheKeys.LatestPosts);
        localCacheManager.Remove(CacheKeys.PopularPosts);
        localCacheManager.Remove(CacheKeys.PostBySlug(post.Slug));

        return post.Id;
    }
}
