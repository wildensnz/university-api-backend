using System.ComponentModel.DataAnnotations;


namespace university_api_backend.Models.DataModels
{
    public class Chapter: BaseEntity
    {
        public int CourseId { get; set; }
        public virtual Course Courses { get; set; } = new Course();


        [Required]
        public string List = string.Empty;    
    }
}
