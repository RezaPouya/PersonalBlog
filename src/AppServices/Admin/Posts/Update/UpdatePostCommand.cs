using PersonalBlog.Utilities.Extensions;
using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Posts.Update;

public class UpdatePostCommand
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "عنوان اجباری است")]
    [MaxLength(250)]
    public string Title { get; set; } = default!;

    [Required]
    [MaxLength(250)]
    public string Slug { get; set; } = default!;

    [Required]
    [MaxLength(500)]
    public string Summary { get; set; } = default!;

    [Required]
    public string Content { get; set; } = default!;

    public string? CoverImageUrl { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public bool IsPublished { get; set; }
    public bool IsCommentsEnabled { get; set; } = true;
    public bool IsInEnglish { get; set; }

    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgImageUrl { get; set; }

    public List<int> TagIds { get; set; } = new();

    public UpdatePostCommand Sanitize()
    {
        Title = Title.StringNormalization();
        Slug = Slug.StringNormalization();
        Summary = Summary.StringNormalization();
        MetaTitle = MetaTitle?.StringNormalization();
        MetaDescription = MetaDescription?.StringNormalization();
        return this;
    }
}
