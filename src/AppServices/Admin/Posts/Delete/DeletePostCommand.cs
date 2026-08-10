using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Posts.Delete;

public class DeletePostCommand
{
    [Required]
    public int Id { get; set; }
}
