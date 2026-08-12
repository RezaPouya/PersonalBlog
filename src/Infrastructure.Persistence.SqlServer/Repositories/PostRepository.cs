global using Infrastructure.Persistence.SqlServer.DbExtensions;
using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Entities.Posts.Dtos;
using PersonalBlog.Utilities.Dtos;

namespace Infrastructure.Persistence.SqlServer.Repositories;

public class PostRepository(AppDbContext dbContext) : RepositoryBase<Post>(dbContext), IPostRepository
{
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
}
