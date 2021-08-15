using Microsoft.EntityFrameworkCore;
using SeaInk.Core.Entities;

namespace Infrastructure.Database
{
    public sealed class DatabaseContext: DbContext
    {
        public DbSet<User> Users { get; private set; }
        public DbSet<Student> Students { get; private set; }
        public DbSet<Mentor> Mentors { get; private set; }

        public DbSet<Division> Divisions { get; private set; }
        public DbSet<Subject> Subjects { get; private set; }
        public DbSet<StudyGroup> StudyGroups { get; private set; }

        public DbSet<StudyAssignment> StudyAssignments { get; private set; }
        public DbSet<StudentAssignmentProgress> StudentAssignmentProgresses { get; private set; }

        public DatabaseContext(DbContextOptions options): base(options)
        {
            Database.EnsureCreated();
        }

        public static DatabaseContext GetTestContext(string name)
        {
            DbContextOptions options = GetTestOptions(name);

            return new DatabaseContext(options);
        }

        public static DbContextOptions GetTestOptions(string name)
        {
            DbContextOptions options = ConfigureTestBuilder(new DbContextOptionsBuilder(), name)
                .Options;

            return options;
        }

        public static DbContextOptionsBuilder ConfigureTestBuilder(DbContextOptionsBuilder builder, string name)
        {
            return builder
                .UseInMemoryDatabase(name)
                .UseLazyLoadingProxies();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasOne(s => s.Group);
            
            modelBuilder.Entity<StudyGroup>().HasOne(g => g.Admin);
            modelBuilder.Entity<StudyGroup>().HasMany(g => g.Students);

            modelBuilder.Entity<StudentAssignmentProgress>().OwnsOne(s => s.Progress);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}