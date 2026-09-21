using System.ComponentModel.DataAnnotations;
using Utilities.Extensions;

namespace AppServices.Admin.Projects.Create;

public class CreateProjectCommand : ICommand<int>
{
    [Required(ErrorMessage = "عنوان اجباری است")]
    [MaxLength(200)]
    public string Title { get; set; } = default!;

    [Required(ErrorMessage = "اسلاگ اجباری است")]
    [MaxLength(200)]
    public string Slug { get; set; } = default!;

    [Required(ErrorMessage = "توضیحات اجباری است")]
    [MaxLength(4000)]
    public string Description { get; set; } = default!;

    [MaxLength(2048)]
    public string? ImageUrl { get; set; }

    [MaxLength(2048)]
    public string? LiveUrl { get; set; }

    [MaxLength(2048)]
    public string? RepoUrl { get; set; }

    [MaxLength(500)]
    public string? TechnologiesCsv { get; set; }

    public int OrderInProjects { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsInEnglish { get; set; }

    public CreateProjectCommand Sanitize()
    {
        Title = Title.StringNormalization();
        Slug = Slug.StringNormalization();
        Description = Description.StringNormalization();
        ImageUrl = ImageUrl?.StringNormalization();
        return this;
    }
}
