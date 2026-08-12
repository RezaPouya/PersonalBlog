namespace PersonalBlog.Domain.Entities.Tags.Dtos;

public class TagGridDto
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public int PostsCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}