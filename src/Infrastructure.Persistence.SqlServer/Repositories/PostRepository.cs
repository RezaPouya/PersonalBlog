global using Infrastructure.Persistence.SqlServer.DbExtensions;
using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Entities.Posts.Dtos;
using PersonalBlog.Domain.Entities.Posts.Entities;
using Utilities.Dtos;

namespace Infrastructure.Persistence.SqlServer.Repositories;

public class PostRepository(AppDbContext dbContext) : RepositoryBase<Post>(dbContext), IPostRepository
{
    public async Task<List<IdTitleDto<int>>> GetListForLookupAsync(CancellationToken cancellationToken)
    {
        return await base.DbContext.Posts.AsNoTracking()
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new IdTitleDto<int> { Id = p.Id, Title = p.Title })
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> DoesCategoryHaveAnyPost(int categoryId, CancellationToken cancellationToken)
    {
        var result = await base.DbContext.Posts.AsNoTracking()
            .Where(p => p.CategoryId == categoryId)
            .AnyAsync(cancellationToken);

        return result;
    }

    public async Task<GridDataSourceResult<PostGridDto>> GetGridAsync(GridDataSourceRequest request, CancellationToken cancellationToken)
    {
        var query = base.DbContext.Posts.AsNoTracking()
            .Select(p => new PostGridDto
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                CategoryTitle = p.Category.Title,
                PublishedAt = p.PublishedAt,
                Slug = p.Slug,
                Title = p.Title,
                CreatedAt = p.CreatedAt,
                DeletedAt = p.DeletedAt,
                IsCommentsEnabled = p.IsCommentsEnabled,
                IsDeleted = p.IsDeleted,
                IsInEnglish = p.IsInEnglish,
                IsPublished = p.IsPublished,
                PostCommentsCount = p.Comments.Count(),
                ViewCount = p.ViewCount,
                Summary = p.Summary,
                UpdatedAt = p.UpdatedAt,
            });

        var result = await query.ToDataSourceResult(request, cancellationToken);

        return result;
    }

    public async Task<PostDto?> GetInfoByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await base.DbContext.Posts.AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new PostDto
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                CategoryTitle = p.Category.Title,
                OgImageUrl = p.OgImageUrl,
                PublishedAt = p.PublishedAt,
                Slug = p.Slug,
                TinyUrl = p.TinyUrl,
                Title = p.Title,
                Content = p.Content,
                CoverImageUrl = p.CoverImageUrl,
                CreatedAt = p.CreatedAt,
                DeletedAt = p.DeletedAt,
                IsCommentsEnabled = p.IsCommentsEnabled,
                IsDeleted = p.IsDeleted,
                IsInEnglish = p.IsInEnglish,
                IsPublished = p.IsPublished,
                MetaDescription = p.MetaDescription,
                MetaTitle = p.MetaTitle,
                PostCommentsCount = p.Comments.Count(),
                RelatedPosts = p.RelatedPosts,
                ViewCount = p.ViewCount,
                Summary = p.Summary,
                UpdatedAt = p.UpdatedAt,
                TagIds = p.PostTags.Select(p => p.TagId).ToList(),
            }).
            FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<bool> IsExistsBySlugAsync(string slug, int? id, CancellationToken cancellationToken)
    {
        var query = base.DbContext.Posts.AsNoTracking();

        if (id is not null && id.Value > 0)
        {
            query = query.Where(p => p.Slug == slug && p.Id != id.Value);
        }
        else
        {
            query = query.Where(p => p.Slug == slug);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task ReplaceTagsAsync(int postId, IEnumerable<int> tagIds, CancellationToken cancellationToken)
    {
        var normalizedTagIds = tagIds.Where(x => x > 0).Distinct().ToList();

        var existingTags = await DbContext.PostTags
            .Where(x => x.PostId == postId)
            .ToListAsync(cancellationToken);

        DbContext.PostTags.RemoveRange(existingTags);

        foreach (var tagId in normalizedTagIds)
        {
            DbContext.PostTags.Add(
                new PostTag
                {
                    PostId = postId,
                    TagId = tagId
                });
        }
    }

    // ============================ سمت سایت عمومی ============================

    public async Task<List<PostSummaryDto>> GetLatestPublishedAsync(int count, bool? isInEnglish, CancellationToken cancellationToken)
    {
        var query = base.DbContext.Posts.AsNoTracking()
            .Where(p => !p.IsDeleted && p.IsPublished);

        if (isInEnglish.HasValue)
            query = query.Where(p => p.IsInEnglish == isInEnglish.Value);

        return await query
            .OrderByDescending(p => p.PublishedAt ?? p.CreatedAt)
            .Take(count)
            .Select(p => new PostSummaryDto
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                Summary = p.Summary,
                CoverImageUrl = p.CoverImageUrl,
                PublishedAt = p.PublishedAt,
                CategoryTitle = p.Category.Title,
                CategorySlug = p.Category.Slug,
                ViewCount = p.ViewCount
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PostSiteDetailDto?> GetPublishedBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        return await base.DbContext.Posts.AsNoTracking()
            .Where(p => !p.IsDeleted && p.IsPublished && p.Slug == slug)
            .Select(p => new PostSiteDetailDto
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                Summary = p.Summary,
                Content = p.Content,
                CoverImageUrl = p.CoverImageUrl,
                PublishedAt = p.PublishedAt,
                CategoryTitle = p.Category.Title,
                CategorySlug = p.Category.Slug,
                ViewCount = p.ViewCount,
                IsCommentsEnabled = p.IsCommentsEnabled,
                MetaTitle = p.MetaTitle,
                MetaDescription = p.MetaDescription,
                Tags = p.PostTags.OrderBy(pt => pt.TagId).Select(pt => pt.Tag.Title).ToList(),
                TagIds = p.PostTags.OrderBy(pt => pt.TagId).Select(pt => pt.TagId).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PostListPageDto> GetPublishedListAsync(int page, int pageSize, int? categoryId, int? tagId, CancellationToken cancellationToken)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var query = base.DbContext.Posts.AsNoTracking()
            .Where(p => !p.IsDeleted && p.IsPublished);

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        if (tagId.HasValue)
            query = query.Where(p => p.PostTags.Any(pt => pt.TagId == tagId.Value));

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(p => p.PublishedAt ?? p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new PostSummaryDto
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                Summary = p.Summary,
                CoverImageUrl = p.CoverImageUrl,
                PublishedAt = p.PublishedAt,
                CategoryTitle = p.Category.Title,
                CategorySlug = p.Category.Slug,
                ViewCount = p.ViewCount
            })
            .ToListAsync(cancellationToken);

        return new PostListPageDto { Items = items, Page = page, PageSize = pageSize, Total = total };
    }

    public async Task IncrementViewCountAsync(int postId, CancellationToken cancellationToken)
    {
        // آپدیت مستقیم و اتمیک، بدون نیاز به لود کردن کل موجودیت.
        await base.DbContext.Posts
            .Where(p => p.Id == postId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(p => p.ViewCount, p => p.ViewCount + 1), cancellationToken);
    }

    public async Task<List<SitemapUrlDto>> GetAllPublishedForSitemapAsync(CancellationToken cancellationToken)
    {
        return await base.DbContext.Posts.AsNoTracking()
            .Where(p => !p.IsDeleted && p.IsPublished)
            .Select(p => new SitemapUrlDto
            {
                Slug = p.Slug,
                UpdatedAt = p.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<List<PostSummaryDto>> GetLatestForFeedAsync(int count, CancellationToken cancellationToken)
    {
        return await base.DbContext.Posts.AsNoTracking()
            .Where(p => !p.IsDeleted && p.IsPublished)
            .OrderByDescending(p => p.PublishedAt ?? p.CreatedAt)
            .Take(count)
            .Select(p => new PostSummaryDto
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                Summary = p.Summary,
                CoverImageUrl = p.CoverImageUrl,
                PublishedAt = p.PublishedAt,
                CategoryTitle = p.Category.Title,
                CategorySlug = p.Category.Slug,
                ViewCount = p.ViewCount
            })
            .ToListAsync(cancellationToken);
    }
}
