using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.CoursePosts.Update
{
    public class UpdateCoursePostCommand
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int PostId { get; set; }

        [MaxLength(200)]
        public string? Title { get; set; }

        [MaxLength(2048)]
        public string? Description { get; set; }

        [MaxLength(2048)]
        public string? CoverImageUrl { get; set; }

        public bool IsPublished { get; set; } = true;

        public int OrderInCourse { get; set; }
    }
}
