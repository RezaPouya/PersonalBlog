namespace PersonalBlog.Domain.Entities.Posts.Dtos;

/// <summary>یک صفحه از لیست پست‌های منتشرشده (برای آرشیو/دسته‌بندی/تگ در سایت عمومی).</summary>
public class PostListPageDto
{
    public List<PostSummaryDto> Items { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(Total / (double)PageSize);
}
