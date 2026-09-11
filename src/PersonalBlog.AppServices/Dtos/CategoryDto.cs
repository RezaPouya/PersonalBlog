namespace PersonalBlog.AppServices.Dtos;

public class CategoryDto
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public int PostCount { get; set; }
}

public class CreateCategoryInputDto
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
}

public class UpdateCategoryInputDto : CreateCategoryInputDto
{
    public long Id { get; set; }
}
