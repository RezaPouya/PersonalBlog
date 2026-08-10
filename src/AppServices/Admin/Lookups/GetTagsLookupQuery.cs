namespace AppServices.Admin.Lookups;

public class GetTagsLookupQuery { }

public class TagLookupItem
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
}

public class GetTagsLookupQueryHandler(Tags.ITagRepository repo)
    : AppServices.Base.IQueryHandler<GetTagsLookupQuery, List<TagLookupItem>>
{
    public Task<List<TagLookupItem>> Invoke(GetTagsLookupQuery input, CancellationToken cancellationToken)
    {
        var list = repo.Query()
            .OrderBy(t => t.Title)
            .Select(t => new TagLookupItem { Id = t.Id, Title = t.Title })
            .ToList();


        return Task.FromResult(list);
    }
}
