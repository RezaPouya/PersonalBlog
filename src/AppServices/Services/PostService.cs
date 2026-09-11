//using Ganss.Xss;
//using PersonalBlog.AppServices.Dtos;
//using PersonalBlog.Domain.Commons;
//using PersonalBlog.Domain.Constants;
//using PersonalBlog.Domain.Entities.Posts;
//using PersonalBlog.Domain.Entities.Posts.Entities;

//namespace PersonalBlog.AppServices.Services.Blog.Imps;

//public class PostService(
//    IRepository<Post> postRepository,
//    IRepository<PostTag> postTagRepository,
//    IRepository<Comment> commentRepository,
//    IUnitOfWork unitOfWork,
//    ILocalCacheManager cache) : IPostService
//{
//    private static readonly HtmlSanitizer Sanitizer = CreateSanitizer();

//    public async Task<(List<PostListItemDto> Items, int TotalCount)> GetListAsync(PostListFilterDto filter, CancellationToken cancellationToken = default)
//    {
//        var query = postRepository.Query().Where(p => !p.IsDeleted);
//        if (filter.OnlyPublished) query = query.Where(p => p.IsPublished);
//        if (filter.CategoryId.HasValue) query = query.Where(p => p.CategoryId == filter.CategoryId.Value);
//        if (filter.CourseId.HasValue) query = query.Where(p => p.CourseId == filter.CourseId.Value);
//        if (filter.TagId.HasValue) query = query.Where(p => p.PostTags.Any(pt => pt.TagId == filter.TagId.Value));

//        var totalCount = await query.CountAsync(cancellationToken);
//        var items = await query
//            .OrderByDescending(p => p.PublishedAt ?? p.CreatedAt)
//            .Skip((filter.Page - 1) * filter.PageSize)
//            .Take(filter.PageSize)
//            .Select(p => MapToListItem(p))
//            .ToListAsync(cancellationToken);

//        return (items, totalCount);
//    }

//    public async Task<PostDetailDto?> GetBySlugAsync(string slug, bool onlyApprovedComments = true, CancellationToken cancellationToken = default)
//    {
//        return await cache.GetOrCreateAsync(CacheKeys.PostBySlug(slug), async () =>
//        {
//            var post = await postRepository.Query().Where(p => p.Slug == slug && !p.IsDeleted).FirstOrDefaultAsync(cancellationToken);
//            if (post is null) return null;
//            return await MapToDetailAsync(post.Id, onlyApprovedComments, cancellationToken);
//        }, timeOutInSeconds: 300, cancellationToken);
//    }

//    public async Task<PostDetailDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
//        => await MapToDetailAsync(id, onlyApprovedComments: false, cancellationToken);

//    public async Task<long> CreateAsync(CreatePostInputDto input, CancellationToken cancellationToken = default)
//    {
//        var entity = new Post
//        {
//            Title = input.Title,
//            Slug = input.Slug,
//            Summary = input.Summary,
//            Content = Sanitizer.Sanitize(input.Content),
//            CoverImageUrl = input.CoverImageUrl,
//            IsPublished = input.IsPublished,
//            PublishedAt = input.IsPublished ? DateTime.UtcNow : null, // UtcNow!
//            CategoryId = input.CategoryId,
//            CourseId = input.CourseId,
//            OrderInCourse = input.OrderInCourse,
//            MetaTitle = input.MetaTitle,
//            MetaDescription = input.MetaDescription,
//            OgImageUrl = input.OgImageUrl,
//        };

//        postRepository.Add(entity);
//        await unitOfWork.SaveChangesAsync(cancellationToken);
//        await SyncTagsAsync(entity.Id, input.TagIds, cancellationToken);
//        InvalidatePostCaches(entity.Slug);
//        return entity.Id;
//    }

//    public async Task UpdateAsync(UpdatePostInputDto input, CancellationToken cancellationToken = default)
//    {
//        var entity = await postRepository.FindByIdAsync(input.Id) ?? throw new KeyNotFoundException("پست یافت نشد.");
//        var oldSlug = entity.Slug;
//        var wasPublished = entity.IsPublished;

//        entity.Title = input.Title; entity.Slug = input.Slug; entity.Summary = input.Summary;
//        entity.Content = Sanitizer.Sanitize(input.Content); entity.CoverImageUrl = input.CoverImageUrl;
//        entity.IsPublished = input.IsPublished; entity.CategoryId = input.CategoryId;
//        entity.CourseId = input.CourseId; entity.OrderInCourse = input.OrderInCourse;
//        entity.MetaTitle = input.MetaTitle; entity.MetaDescription = input.MetaDescription; entity.OgImageUrl = input.OgImageUrl;

//        if (!wasPublished && input.IsPublished) entity.PublishedAt = DateTime.UtcNow;
//        if (!input.IsPublished) entity.PublishedAt = null;

//        postRepository.Update(entity);
//        await unitOfWork.SaveChangesAsync(cancellationToken);
//        await SyncTagsAsync(entity.Id, input.TagIds, cancellationToken);
//        InvalidatePostCaches(oldSlug);
//        InvalidatePostCaches(entity.Slug);
//    }

//    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
//    {
//        var entity = await postRepository.FindByIdAsync(id);
//        if (entity is null) return;
//        entity.MarkAsDeleted(); // Uses the simplified Domain method
//        postRepository.Update(entity);
//        await unitOfWork.SaveChangesAsync(cancellationToken);
//        InvalidatePostCaches(entity.Slug);
//    }

//    public async Task<bool> IsSlugAvailableAsync(string slug, long? exceptPostId = null, CancellationToken cancellationToken = default)
//    {
//        var query = postRepository.Query().Where(p => p.Slug == slug);
//        if (exceptPostId.HasValue) query = query.Where(p => p.Id != exceptPostId.Value);
//        return !await query.AnyAsync(cancellationToken);
//    }

//    // --- Private Helpers ---
//    private async Task SyncTagsAsync(long postId, List<long> tagIds, CancellationToken cancellationToken)
//    {
//        var existingMaps = await postTagRepository.Query(asNoTracking: false).Where(pt => pt.PostId == postId).ToListAsync(cancellationToken);
//        foreach (var map in existingMaps.Where(m => !tagIds.Contains(m.TagId))) postTagRepository.Delete(map);
//        var existingTagIds = existingMaps.Select(m => m.TagId).ToHashSet();
//        foreach (var tagId in tagIds.Where(id => !existingTagIds.Contains(id))) postTagRepository.Add(new PostTag { PostId = postId, TagId = tagId });
//        await unitOfWork.SaveChangesAsync(cancellationToken);
//    }

//    private async Task<PostDetailDto?> MapToDetailAsync(long postId, bool onlyApprovedComments, CancellationToken cancellationToken)
//    {
//        var post = await postRepository.Query().Where(p => p.Id == postId).Select(p => new PostDetailDto
//        {
//            Id = p.Id,
//            Title = p.Title,
//            Slug = p.Slug,
//            Summary = p.Summary,
//            Content = p.Content,
//            CoverImageUrl = p.CoverImageUrl,
//            IsPublished = p.IsPublished,
//            PublishedAt = p.PublishedAt,
//            ViewCount = p.ViewCount,
//            CategoryId = p.CategoryId,
//            CategoryTitle = p.Category.Title,
//            CourseId = p.CourseId,
//            CourseTitle = p.Course != null ? p.Course.Title : null,
//            MetaTitle = p.MetaTitle,
//            MetaDescription = p.MetaDescription,
//            OgImageUrl = p.OgImageUrl,
//            Tags = p.PostTags.Select(pt => pt.Tag.Title).ToList()
//        }).FirstOrDefaultAsync(cancellationToken);

//        if (post is null) return null;

//        var commentsQuery = commentRepository.Query().Where(c => c.PostId == postId && c.ParentCommentId == null && !c.IsSpam);

//        if (onlyApprovedComments) commentsQuery = commentsQuery.Where(c => c.IsApproved);

//        post.Comments = await commentsQuery.OrderBy(c => c.CreatedAt).Select(c => new CommentDto
//        {
//            Id = c.Id,
//            DisplayName = c.DisplayName,
//            Content = c.Content,
//            CreatedAt = c.CreatedAt,
//            IsApproved = c.IsApproved,
//            Replies = c.Replies.Where(r => !r.IsSpam && (!onlyApprovedComments || r.IsApproved)).OrderBy(r => r.CreatedAt)
//                .Select(r => new CommentDto { Id = r.Id, DisplayName = r.DisplayName, Content = r.Content, CreatedAt = r.CreatedAt, IsApproved = r.IsApproved }).ToList()
//        }).ToListAsync(cancellationToken);

//        return post;
//    }

//    private static PostListItemDto MapToListItem(Post p) => new()
//    {
//        Id = p.Id,
//        Title = p.Title,
//        Slug = p.Slug,
//        Summary = p.Summary,
//        CoverImageUrl = p.CoverImageUrl,
//        IsPublished = p.IsPublished,
//        PublishedAt = p.PublishedAt,
//        ViewCount = p.ViewCount,
//        CategoryTitle = p.Category.Title,
//        Tags = p.PostTags.Select(pt => pt.Tag.Title).ToList()
//    };

//    private void InvalidatePostCaches(string slug)
//    {
//        cache.Remove(CacheKeys.PostBySlug(slug));
//        cache.Remove(CacheKeys.LatestPosts);
//        cache.Remove(CacheKeys.PopularPosts);
//    }

//    private static HtmlSanitizer CreateSanitizer()
//    {
//        var sanitizer = new HtmlSanitizer();
//        sanitizer.AllowedTags.Add("pre"); sanitizer.AllowedTags.Add("code");
//        sanitizer.AllowedAttributes.Add("class"); // For syntax highlighting
//        return sanitizer;
//    }
//}