using System.ComponentModel.DataAnnotations;

namespace university_api_backend.Models.DataModels

{
    public class Categories: BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
