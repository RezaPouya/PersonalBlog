using PersonalBlog.Utilities.Extensions;
using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Tags.Update;

public class UpdateTagCommand
{
    [Required(ErrorMessage = "شناسه اجباری است")]
    public int Id { get; set; }

    [Required(ErrorMessage = "عنوان اجباری است")]
    [MaxLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
    public string Title { get; set; } = default!;

    public UpdateTagCommand Sanitize()
    {
        Title = Title.StringNormalization();

        return this;
    }
}