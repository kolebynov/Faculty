using Faculty.EFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace Faculty.EFCore.Data
{
    public class FacultyContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public FacultyContext(DbContextOptions<FacultyContext> options) : base(options)
        { }
    }
}