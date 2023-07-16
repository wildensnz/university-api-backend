using Microsoft.EntityFrameworkCore;
using university_api_backend.Models.DataModels;

namespace university_api_backend.DataAccess
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options): base(options)
        {

        }

        //TODO: Add db sets (table our data base)

        public DbSet<Users>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Categories>? Categories { get; set; }
        public DbSet<Student>? Students { get; set; }    
        public DbSet<Chapter>? Chapters { get; set; }    
    }
}
