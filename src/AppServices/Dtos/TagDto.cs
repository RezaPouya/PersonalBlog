namespace AppServices.Dtos;

// --- Tags ---
public class TagDto { 
    public long Id { get; set; } 
    public string Title { get; set; } = default!; 
    public string Slug { get; set; } = default!; 
    public int PostCount { get; set; } 
}
