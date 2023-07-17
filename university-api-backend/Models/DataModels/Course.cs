using System.ComponentModel.DataAnnotations;

namespace university_api_backend.Models.DataModels
{

    public enum Level
    {
        Basic,
        Medium,
        Advanced,
        Expert
    }
    public class Course : BaseEntity
    {
        [Required,StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required,StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        public string LargeDescription { get; set; } = string.Empty;
        public string TargetAudiences { get; set; } = string.Empty;
        public string Goals { get; set; } = string.Empty;
        public string Requirements { get; set;} = string.Empty;
        public Level Level { get; set; } = Level.Basic;

        [Required]
        public ICollection<Categories> Categories { get; set; } = new List<Categories>();
        [Required]
        public Chapter Chapters { get; set; } = new Chapter();    
        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();


    }




}
