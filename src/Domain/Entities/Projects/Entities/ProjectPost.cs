using PersonalBlog.Domain.Entities.Posts;

namespace PersonalBlog.Domain.Entities.Projects.Entities
{
    public class ProjectPost : EntityBase
    {
        public int PostId { get; set; }
        public int ProjectId { get; set; }
        public string? Title { get; set; } = default!;
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public bool IsPublished { get; set; } = true;
        public int OrderInCourse { get; set; }
        public virtual Project Project { get; set; }
        public virtual Post Post { get; set; }


    }
}
