using PersonalBlog.Utilities.Extensions;
using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Posts.Create;

public class CreatePostCommand
{
    [Required(ErrorMessage = "عنوان اجباری است")]
    [MaxLength(250, ErrorMessage = "حداکثر 250 کاراکتر")]
    public string Title { get; set; } = default!;

    [Required(ErrorMessage = "اسلاگ اجباری است")]
    [MaxLength(250, ErrorMessage = "حداکثر 250 کاراکتر")]
    public string Slug { get; set; } = default!;

    [Required(ErrorMessage = "خلاصه اجباری است")]
    [MaxLength(500, ErrorMessage = "حداکثر 500 کاراکتر")]
    public string Summary { get; set; } = default!;

    [Required(ErrorMessage = "محتوا اجباری است")]
    public string Content { get; set; } = default!;

    public string? CoverImageUrl { get; set; }

    [Required(ErrorMessage = "دسته‌بندی اجباری است")]
    public int CategoryId { get; set; }

    public bool IsPublished { get; set; }
    public bool IsCommentsEnabled { get; set; } = true;
    public bool IsInEnglish { get; set; }

    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgImageUrl { get; set; }

    public List<int> TagIds { get; set; } = new();

    public CreatePostCommand Sanitize()
    {
        Title = Title.StringNormalization();
        Slug = Slug.StringNormalization();
        Summary = Summary.StringNormalization();
        MetaTitle = MetaTitle?.StringNormalization();
        MetaDescription = MetaDescription?.StringNormalization();
        return this;
    }
}
