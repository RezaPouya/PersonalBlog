using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Services.Common;

public interface ISearchService
{
    Task<List<SearchResultDto>> SearchAsync(string query, int page = 1, int pageSize = 10,
        CancellationToken cancellationToken = default);
}
