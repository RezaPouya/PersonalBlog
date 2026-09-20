using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.ContactMessages.Delete;

public class DeleteContactMessageCommand : ICommand<int>
{
    [Required(ErrorMessage = "شناسه اجباری است")]
    public int Id { get; set; }
}
