namespace PersonalBlog.AppServices.Dtos;

public class PostListFilterDto { public int Page { get; set; } = 1; public int PageSize { get; set; } = 12; public long? CategoryId { get; set; } public long? TagId { get; set; } public long? CourseId { get; set; } public bool OnlyPublished { get; set; } = true; }
