using AppServices.Commons;
using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Posts.Update;

public class UpdatePostCommandHandler(
    IValidator<UpdatePostCommand> validator,
    ILocalCacheManager localCacheManager,
    IPostRepository postRepository,
    ICategoryRepository categoryRepository,
    IHtmlSanitizerService htmlSanitizerService,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdatePostCommand, int>
{
    public async Task<int> Invoke(UpdatePostCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        input.Sanitize();

        var post = await postRepository.FindByIdAsync(input.Id, cancellationToken)
            ?? throw new BusinessException("پست یافت نشد.");

        if (!await categoryRepository.IsExistsByIdAsync(input.CategoryId, cancellationToken))
            throw new BusinessException("دسته‌بندی یافت نشد.");

        if (await postRepository.IsExistsBySlugAsync(input.Slug, input.Id, cancellationToken))
            throw new BusinessException("پست دیگری با این اسلاگ وجود دارد.");

        var oldSlug = post.Slug;

        post.Title = input.Title;
        post.Slug = input.Slug;
        post.Summary = input.Summary;
        post.CoverImageUrl = input.CoverImageUrl;
        post.CategoryId = input.CategoryId;
        post.IsCommentsEnabled = input.IsCommentsEnabled;
        post.IsInEnglish = input.IsInEnglish;
        post.MetaTitle = input.MetaTitle;
        post.MetaDescription = input.MetaDescription;
        post.OgImageUrl = input.OgImageUrl;

        post.Content = htmlSanitizerService.Sanitize(input.Content);

        if (input.IsPublished && !post.IsPublished)
        {
            post.IsPublished = true;
            post.PublishedAt = DateTime.Now;
        }
        else if (!input.IsPublished)
        {
            post.IsPublished = false;
            post.PublishedAt = null;
        }

        await postRepository.ReplaceTagsAsync(post.Id, input.TagIds, cancellationToken);
        postRepository.SetUpdatedAt(post);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        localCacheManager.Remove(CacheKeys.LatestPosts);
        localCacheManager.Remove(CacheKeys.PopularPosts);
        localCacheManager.Remove(CacheKeys.PostBySlug(oldSlug));
        localCacheManager.Remove(CacheKeys.PostBySlug(post.Slug));

        return post.Id;
    }
}
