using AppServices.Commons;
using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Entities.Posts.Entities;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Posts.Create;

public class CreatePostCommandHandler(
    IValidator<CreatePostCommand> validator,
    ILocalCacheManager localCacheManager,
    IPostRepository postRepository,
    ICategoryRepository categoryRepository,
    IHtmlSanitizerService htmlSanitizerService,
    IUnitOfWork unitOfWork) : ICommandHandler<CreatePostCommand, int>
{
    public async Task<int> Invoke(CreatePostCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        input.Sanitize();

        if (!await categoryRepository.IsExistsByIdAsync(input.CategoryId, cancellationToken))
            throw new BusinessException("دسته‌بندی یافت نشد.");

        if (await postRepository.IsExistsBySlugAsync(input.Slug, null, cancellationToken))
            throw new BusinessException("پست با این اسلاگ قبلاً ثبت شده است.");

        var post = new Post
        {
            Title = input.Title,
            Slug = input.Slug,
            Summary = input.Summary,
            Content = input.Content,
            CoverImageUrl = input.CoverImageUrl,
            CategoryId = input.CategoryId,
            IsPublished = input.IsPublished,
            PublishedAt = input.IsPublished ? DateTime.Now : null,
            IsCommentsEnabled = input.IsCommentsEnabled,
            IsInEnglish = input.IsInEnglish,
            MetaTitle = input.MetaTitle,
            MetaDescription = input.MetaDescription,
            OgImageUrl = input.OgImageUrl
        };

        foreach (var tagId in input.TagIds.Distinct())
            post.PostTags.Add(new PostTag { TagId = tagId });

        post.Content = htmlSanitizerService.Sanitize(input.Content);

        postRepository.Create(post);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        localCacheManager.Remove(CacheKeys.LatestPosts);
        localCacheManager.Remove(CacheKeys.PopularPosts);
        localCacheManager.Remove(CacheKeys.CategoriesList);

        return post.Id;
    }
}
