using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Tags.Delete;

public class DeleteTagCommand
{
    [Required(ErrorMessage = "شناسه اجباری است")]
    public int Id { get; set; }
}