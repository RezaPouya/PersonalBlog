namespace PersonalBlog.AppServices.Dtos;

// --- Categories ---
public class CategoryDto { 
    public long Id { get; set; } 
    public string Title { get; set; } = default!; 
    public string Slug { get; set; } = default!; 
    public string? Description { get; set; } 
    public int PostCount { get; set; }
}
