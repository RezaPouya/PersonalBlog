namespace PersonalBlog.AppServices.Dtos;

public class PostListItemDto
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }
    public int ViewCount { get; set; }
    public string CategoryTitle { get; set; } = default!;
    public List<string> Tags { get; set; } = new();
}

public class PostDetailDto : PostListItemDto
{
    public string Content { get; set; } = default!;
    public long CategoryId { get; set; }
    public long? CourseId { get; set; }
    public string? CourseTitle { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgImageUrl { get; set; }
    public List<CommentDto> Comments { get; set; } = new();
}

public class CreatePostInputDto
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public bool IsPublished { get; set; }
    public long CategoryId { get; set; }
    public long? CourseId { get; set; }
    public int? OrderInCourse { get; set; }
    public List<long> TagIds { get; set; } = new();
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgImageUrl { get; set; }
}

public class UpdatePostInputDto : CreatePostInputDto
{
    public long Id { get; set; }
}

public class PostListFilterDto
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;
    public long? CategoryId { get; set; }
    public long? TagId { get; set; }
    public long? CourseId { get; set; }
    public bool OnlyPublished { get; set; } = true;
}
