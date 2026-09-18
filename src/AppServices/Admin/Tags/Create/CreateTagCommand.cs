using System.ComponentModel.DataAnnotations;
using Utilities.Extensions;

namespace AppServices.Admin.Tags.Create;

public class CreateTagCommand : ICommand<int>
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
