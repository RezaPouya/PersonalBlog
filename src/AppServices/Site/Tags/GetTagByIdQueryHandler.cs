using PersonalBlog.Domain.Entities.Tags;

namespace AppServices.Site.Tags;

public class GetTagByIdQueryHandler(ITagRepository tagRepository)
    : IQueryHandler<GetTagByIdQuery, TagHeaderDto?>
{
    public async Task<TagHeaderDto?> Handle(GetTagByIdQuery input, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByIdAsync(input.Id, cancellationToken);
        if (tag is null) return null;

        return new TagHeaderDto { Id = tag.Id, Title = tag.Title };
    }
}
