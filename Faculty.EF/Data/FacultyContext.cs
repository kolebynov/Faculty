using Faculty.EFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace Faculty.EFCore.Data
{
    public class FacultyContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Domain.Faculty> Faculties { get; set; }
        public DbSet<Cathedra> Cathedras { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public FacultyContext(DbContextOptions<FacultyContext> options) : base(options)
        { }
    }
}