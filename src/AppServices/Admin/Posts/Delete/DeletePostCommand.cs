using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Posts.Delete;

public class DeletePostCommand : ICommand<int>
{
    [Required]
    public int Id { get; set; }
}
