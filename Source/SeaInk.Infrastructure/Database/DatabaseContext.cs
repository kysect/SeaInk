using Microsoft.EntityFrameworkCore;
using SeaInk.Core.Entities;

namespace SeaInk.Infrastructure.Database
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; private set; } = null!;
        public DbSet<Student> Students { get; private set; } = null!;
        public DbSet<Mentor> Mentors { get; private set; } = null!;

        public DbSet<Division> Divisions { get; private set; } = null!;
        public DbSet<Subject> Subjects { get; private set; } = null!;
        public DbSet<StudyGroup> StudyGroups { get; private set; } = null!;
        public DbSet<StudyGroupSubject> StudyGroupSubjects { get; private set; } = null!;
        public DbSet<StudyAssignment> StudyAssignments { get; private set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().HasMany<StudyAssignment>("_assignments");

            modelBuilder.Entity<StudyGroupSubject>().HasMany<Mentor>("_mentors");
            modelBuilder.Entity<StudyGroupSubject>().HasOne(s => s.Subject);
            modelBuilder.Entity<StudyGroupSubject>().HasOne(s => s.StudyGroup);

            modelBuilder.Entity<StudyGroup>().HasMany<Student>("_students");

            modelBuilder.Entity<Student>().HasOne(s => s.Group);

            modelBuilder.Entity<StudyGroupSubject>().HasMany<Mentor>("_mentors");
            modelBuilder.Entity<StudyGroupSubject>().HasOne(s => s.StudyGroup);
            modelBuilder.Entity<StudyGroupSubject>().HasOne(s => s.Subject);

            modelBuilder.Entity<Division>().HasMany<StudyGroupSubject>("_studyGroupSubjects");
        }
    }
}