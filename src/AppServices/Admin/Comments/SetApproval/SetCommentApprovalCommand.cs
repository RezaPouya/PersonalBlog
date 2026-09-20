using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Comments.SetApproval;

public class SetCommentApprovalCommand : ICommand<int>
{
    [Required(ErrorMessage = "شناسه اجباری است")]
    public int Id { get; set; }

    public bool IsApproved { get; set; }
}
