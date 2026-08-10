using System.ComponentModel.DataAnnotations;

public class DeleteCategoryCommand
{
    [Required(ErrorMessage = "شناسه اجباری است")]
    public int Id { get; set; }
}

