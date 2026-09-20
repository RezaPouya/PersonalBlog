using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Projects.Delete;

public class DeleteProjectCommand : ICommand<int>
{
    [Required(ErrorMessage = "شناسه اجباری است")]
    public int Id { get; set; }
}
