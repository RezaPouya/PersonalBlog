using PersonalBlog.Domain.Entities.Tags;
using PersonalBlog.Domain.Entities.Tags.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.Tags.Grid;

public class GetTagListAsGridQueryHandler(
    ITagRepository tagRepository)
    : IQueryHandler<
        GetTagListAsGridQuery,
        GridDataSourceResult<TagGridDto>>
{
    public async Task<GridDataSourceResult<TagGridDto>> Handle(
        GetTagListAsGridQuery input,
        CancellationToken cancellationToken)
    {
        return await tagRepository.GetGridAsync(
            input,
            cancellationToken);
    }
}