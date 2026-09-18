namespace AppServices.Admin.CoursePosts.Delete
{
    public class RemovePostFromCourseCommand : ICommand<int>
    {
        public int Id { get; set; }
    }
}
