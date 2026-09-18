using PersonalBlog.Domain.Entities.Posts.Dtos;

namespace AppServices.Admin.Posts.Read;

public class GetPostQuery : IQuery<PostDto>
{
    public int Id { get; set; }
}
