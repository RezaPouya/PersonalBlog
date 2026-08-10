using PersonalBlog.Utilities.Extensions;
using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Tags.Create;

public class CreateTagCommand
{
    [Required(ErrorMessage = "عنوان اجباری است")]
    [MaxLength(100)]
    public string Title { get; set; } = default!;

    [Required(ErrorMessage = "اسلاگ اجباری است")]
    [MaxLength(100)]
    public string Slug { get; set; } = default!;

    public CreateTagCommand Sanitize()
    {
        Title = Title.StringNormalization();
        Slug = Slug.StringNormalization();
        return this;
    }
}
