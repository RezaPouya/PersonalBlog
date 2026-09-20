namespace AppServices.Site.Tags;

public class GetTagByIdQuery : IQuery<TagHeaderDto?>
{
    public int Id { get; set; }
}

public class TagHeaderDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
}
