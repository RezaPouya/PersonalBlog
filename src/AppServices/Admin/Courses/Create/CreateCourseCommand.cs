using PersonalBlog.Utilities.Extensions;
using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Courses.Create;

public class CreateCourseCommand
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = default!;

    [Required]
    [MaxLength(200)]
    public string Slug { get; set; } = default!;

    [MaxLength(2048)]
    public string? Description { get; set; }

    [MaxLength(2048)]
    public string? CoverImageUrl { get; set; }

    public bool IsPublished { get; set; }

    public bool IsInEnglish { get; set; }

    public int OrderInCourses { get; set; }

    public CreateCourseCommand Sanitize()
    {
        Title = Title.StringNormalization();
        Slug = Slug.StringNormalization();
        Description = Description?.StringNormalization();
        CoverImageUrl = CoverImageUrl?.StringNormalization();

        return this;
    }
}