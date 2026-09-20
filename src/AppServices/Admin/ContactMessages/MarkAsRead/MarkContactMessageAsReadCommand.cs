using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.ContactMessages.MarkAsRead;

public class MarkContactMessageAsReadCommand : ICommand<int>
{
    [Required(ErrorMessage = "شناسه اجباری است")]
    public int Id { get; set; }
}
