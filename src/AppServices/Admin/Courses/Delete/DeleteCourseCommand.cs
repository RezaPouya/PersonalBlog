namespace AppServices.Admin.Courses.Delete;

public class DeleteCourseCommand : ICommand<int>
{
    public int Id { get; set; }
}
