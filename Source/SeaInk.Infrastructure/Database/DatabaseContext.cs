using Microsoft.EntityFrameworkCore;
using SeaInk.Core.Entities;

namespace Infrastructure.Database
{
    public class DatabaseContext: DbContext
    {
        public DbSet<User> Users { get; private set; }
        public DbSet<Student> Students { get; private set; }
        public DbSet<Mentor> Mentors { get; private set; }

        public DbSet<Division> Divisions { get; private set; }
        public DbSet<Subject> Subjects { get; private set; }
        public DbSet<StudyGroup> StudyGroups { get; private set; }

        public DbSet<StudyAssignment> StudyAssignments { get; private set; }
        public DbSet<StudentAssignmentProgress> StudentAssignmentProgresses { get; private set; }

        public DatabaseContext(DbContextOptions options): base(options) { }
    }
}