using PersonalBlog.Domain.Entities.Tags;
using Utilities.Dtos;

namespace AppServices.Admin.Lookups;

public class GetTagsLookupQueryHandler(ITagRepository repo) : AppServices.Base.IQueryHandler<GetTagsLookupQuery, List<IdTitleDto<int>>>
{
    public async Task<List<IdTitleDto<int>>> Handle(GetTagsLookupQuery input, CancellationToken cancellationToken)
    {
        return await repo.GetListForLookupAsync(cancellationToken);
    }
}
