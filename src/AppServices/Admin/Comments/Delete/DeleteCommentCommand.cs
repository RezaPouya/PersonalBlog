using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Comments.Delete;

public class DeleteCommentCommand : ICommand<int>
{
    [Required(ErrorMessage = "شناسه اجباری است")]
    public int Id { get; set; }
}
