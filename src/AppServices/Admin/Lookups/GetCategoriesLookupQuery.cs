global using AppServices.Base;
using PersonalBlog.Domain.Entities.Categories;

namespace AppServices.Admin.Lookups;

public class GetCategoriesLookupQuery { }

public class CategoryLookupItem
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
}

public class GetCategoriesLookupQueryHandler(ICategoryRepository repo)
    : IQueryHandler<GetCategoriesLookupQuery, List<CategoryLookupItem>>
{
    public async Task<List<CategoryLookupItem>> Invoke(GetCategoriesLookupQuery input, CancellationToken cancellationToken)
    {
        var list = repo.Query()
            .OrderBy(c => c.Title)
            .Select(c => new CategoryLookupItem { Id = c.Id, Title = c.Title })
            .ToList();

        return await Task.FromResult(list);
    }
}
