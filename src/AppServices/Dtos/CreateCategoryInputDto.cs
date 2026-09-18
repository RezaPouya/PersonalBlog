namespace AppServices.Dtos;

public class CreateCategoryInputDto
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
}
