using PersonalBlog.Utilities.Extensions;
using System.ComponentModel.DataAnnotations;

public class CreateCategoryCommand
{
    [Required(ErrorMessage = "عنوان اجباری است")]
    [MaxLength(100, ErrorMessage = "حداکثر 150 کاراکتر")]
    public string Title { get; set; } = default!;

    [Required(ErrorMessage = "اسلاگ اجباری است")]
    [MaxLength(200, ErrorMessage = "حداکثر 150 کاراکتر")]
    public string Slug { get; set; } = default!;

    [MaxLength(500, ErrorMessage = "حداکثر 2048 کاراکتر")]
    public string? Description { get; set; }

    [Display(Name = "انگلیسی است ؟")]
    public bool IsInEnglish { get; set; }

    public CreateCategoryCommand Sanitize()
    {
        Title = Title.StringNormalization();
        Slug = Slug.StringNormalization();
        Description = Description?.StringNormalization();
        return this;
    }
}

