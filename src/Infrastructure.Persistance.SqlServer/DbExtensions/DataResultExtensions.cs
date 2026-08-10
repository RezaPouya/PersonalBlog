using PersonalBlog.Utilities.Dtos;

namespace Infrastructure.Persistance.SqlServer.DbExtensions;

public static class DataResultExtensions
{
    public static async Task<GridDataSourceResult<T>> ToDataSourceResult<T>(this IQueryable<T> queryable, GridDataSourceRequest request, CancellationToken cancellationToken) where T : class
    {
        var pageSize = request.PageSize < 1 ? 1 : request.PageSize;

        var page = request.Page < 1 ? 1 : request.Page;

        if (request is not null && request.InputParams.Filters is not null && request.InputParams.Filters.Any())
            queryable = queryable.Where(request.InputParams.Filters);

        var totalRecords = await queryable.CountAsync();

        var totalPages = totalRecords / pageSize;

        if (totalRecords % pageSize != 0)
        {
            totalPages++;
        }

        queryable = SortQueryable(queryable, request);

        var skip = (page - 1) * pageSize;

        queryable = queryable.Skip(skip).Take(pageSize);

        var result = await queryable.ToListAsync(cancellationToken);

        return new GridDataSourceResult<T>()
        {
            Data = result,
            PageSize = pageSize,
            Page = page,
            Totals = totalRecords,
            TotalPages = totalPages
        };
    }

    private static IQueryable<T> SortQueryable<T>(IQueryable<T> queryable, GridDataSourceRequest request) where T : class
    {
        // بررسی مستقیم null بودن Sort
        if (request.InputParams.Sort == null)
            return queryable;

        if (string.IsNullOrEmpty(request.InputParams.Sort.PropertyName))
            return queryable;

        return queryable.OrderBy(request.InputParams.Sort);
    }
}