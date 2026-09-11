using Microsoft.EntityFrameworkCore;
using PersonalBlog.AppServices.Dtos;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Blog;

namespace PersonalBlog.AppServices.Services.Common.Imps;

/// <summary>
/// جستجوی پست‌ها بر اساس عنوان/خلاصه/محتوا/تگ/دسته‌بندی.
/// در نسخه‌ی اول با LIKE (EF.Functions.Like)؛ برای مقیاس بزرگ‌تر می‌توان به
/// SQL Server Full-Text Search یا Elasticsearch مهاجرت کرد.
/// </summary>
public class SearchService(IRepository<Post> postRepository) : ISearchService
{
    public async Task<List<SearchResultDto>> SearchAsync(string query, int page = 1, int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<SearchResultDto>();

        var pattern = $"%{query.Trim()}%";
        var skip = (page - 1) * pageSize;

        return await postRepository.Query()
            .Where(p => p.IsPublished && !p.IsDeleted)
            .Where(p =>
                EF.Functions.Like(p.Title, pattern) ||
                EF.Functions.Like(p.Summary, pattern) ||
                EF.Functions.Like(p.Content, pattern) ||
                EF.Functions.Like(p.Category.Title, pattern) ||
                p.PostTags.Any(pt => EF.Functions.Like(pt.Tag.Title, pattern)))
            .OrderByDescending(p => p.PublishedAt)
            .Skip(skip)
            .Take(pageSize)
            .Select(p => new SearchResultDto
            {
                PostId = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                Summary = p.Summary,
                CategoryTitle = p.Category.Title,
                PublishedAt = p.PublishedAt
            })
            .ToListAsync(cancellationToken);
    }
}
