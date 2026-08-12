using PersonalBlog.Utilities.Extensions;
using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Tags.Create;

public class CreateTagCommand
{
    [Required(ErrorMessage = "عنوان اجباری است")]
    [MaxLength(100)]
    public string Title { get; set; } = default!;

    public CreateTagCommand Sanitize()
    {
        Title = Title.StringNormalization();
        return this;
    }
}
