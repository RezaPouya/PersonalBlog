using PersonalBlog.Utilities.Extensions;
using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Courses.Update;

public class UpdateCourseCommand
{
    [Required]
    public int Id { get; set; }

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

    public UpdateCourseCommand Sanitize()
    {
        Title = Title.StringNormalization();
        Slug = Slug.StringNormalization();
        Description = Description?.StringNormalization();
        CoverImageUrl = CoverImageUrl?.StringNormalization();

        return this;
    }
}