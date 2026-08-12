using PersonalBlog.Domain.Entities.Posts;

namespace AppServices.Site.Posts;

public class GetLatestPostsQueryHandler(IPostRepository postRepository)
    : IQueryHandler<GetLatestPostsQuery, List<GetLatestPostsResult>>
{
    public async Task<List<GetLatestPostsResult>> Invoke(GetLatestPostsQuery input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var items = await postRepository.GetLatestPublishedAsync(input.Count, input.IsInEnglish, cancellationToken);

        //return items.Select(p => new GetLatestPostsResult
        //{
        //    Id = p.Id,
        //    Title = p.Title,
        //    Slug = p.Slug,
        //    Summary = p.Summary,
        //    CoverImageUrl = p.CoverImageUrl,
        //    PublishedAt = p.PublishedAt,
        //    CategoryTitle = p.CategoryTitle,
        //    ViewCount = p.ViewCount
        //}).ToList();
    }
}
