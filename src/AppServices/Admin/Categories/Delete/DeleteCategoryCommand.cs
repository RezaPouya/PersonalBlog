using System.ComponentModel.DataAnnotations;

public class DeleteCategoryCommand : ICommand<int>
{
    [Required(ErrorMessage = "شناسه اجباری است")]
    public int Id { get; set; }
}

