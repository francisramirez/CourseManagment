
using Microsoft.EntityFrameworkCore;

namespace CourseManagment.Infraestructure.Context
{
    public partial class CourseMagnamentDbContext : DbContext
    {
        public CourseMagnamentDbContext(DbContextOptions<CourseMagnamentDbContext> options)
            : base(options)
        {

        }

        public DbSet<Domain.Entities.Course> Courses { get; set; }
        public DbSet<Domain.Entities.Department> Departments { get; set; }
        public DbSet<Domain.Entities.Instructor> Instructors { get; set; }
        public DbSet<Domain.Entities.CourseInstructor> CourseInstructors { get; set; }


    }
}
