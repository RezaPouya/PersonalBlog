using PersonalBlog.Domain.Entities.Categories;
using Utilities.Dtos;

namespace AppServices.Admin.Lookups;

public class GetCategoriesLookupQueryHandler(ICategoryRepository repo) : IQueryHandler<GetCategoriesLookupQuery, List<IdTitleDto<int>>>
{
    public async Task<List<IdTitleDto<int>>> Invoke(GetCategoriesLookupQuery input, CancellationToken cancellationToken)
    {
        return await repo.GetListForLookupAsync(cancellationToken);
    }
}
