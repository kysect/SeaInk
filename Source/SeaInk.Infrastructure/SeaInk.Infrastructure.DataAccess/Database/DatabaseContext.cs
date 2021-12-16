using Microsoft.EntityFrameworkCore;
using SeaInk.Core.Entities;
using SeaInk.Core.TableLayout;
using SeaInk.Core.TableLayout.Components;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Infrastructure.DataAccess.Models;

namespace SeaInk.Infrastructure.DataAccess.Database
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Student> Students { get; private set; } = null!;
        public DbSet<Mentor> Mentors { get; private set; } = null!;

        public DbSet<SubjectDivision> SubjectDivisions { get; private set; } = null!;
        public DbSet<Subject> Subjects { get; private set; } = null!;
        public DbSet<StudentGroup> StudentGroups { get; private set; } = null!;
        public DbSet<StudyStudentGroup> StudyStudentGroups { get; private set; } = null!;
        public DbSet<Assignment> Assignments { get; private set; } = null!;

        public DbSet<StudyGroupSubjectLayout> StudyGroupSubjectLayouts { get; private set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureDivision(modelBuilder);
            ConfigureSubject(modelBuilder);
            ConfigureStudyGroup(modelBuilder);
            ConfigureStudyGroupSubject(modelBuilder);
            ConfigureAssignment(modelBuilder);
            ConfigureMentor(modelBuilder);
            ConfigureStudent(modelBuilder);

            // ConfigureTableComponents(modelBuilder);
        }

        private static void ConfigureAssignment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>().Property(x => x.Id).ValueGeneratedNever();
        }

        private static void ConfigureMentor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mentor>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Mentor>()
                .Navigation(x => x.StudyStudentGroups)
                .HasField("_studyStudentGroups");
        }

        private static void ConfigureStudent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(x => x.Id).ValueGeneratedNever();
        }

        private static void ConfigureDivision(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubjectDivision>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<SubjectDivision>().HasOne(d => d.Subject);
            modelBuilder.Entity<SubjectDivision>()
                .Navigation(d => d.StudyStudentGroups)
                .HasField("_studyStudentGroups");
        }

        private static void ConfigureStudyGroup(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentGroup>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<StudentGroup>()
                .Navigation(g => g.Students)
                .HasField("_students");
        }

        private static void ConfigureSubject(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Subject>()
                .Navigation(s => s.Assignments)
                .HasField("_assignments");
        }

        private static void ConfigureStudyGroupSubject(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudyStudentGroup>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<StudyStudentGroup>().HasOne(s => s.StudentGroup);
            modelBuilder.Entity<StudyStudentGroup>().HasOne(ssg => ssg.Division);
            modelBuilder.Entity<StudyStudentGroup>()
                .Navigation(ssg => ssg.Mentors)
                .HasField("_mentors");
        }

        private static void ConfigureTableComponents(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LabelComponent>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity(typeof(CompositeLayoutComponent<>)).HasMany("_components");
            modelBuilder.Entity<LabelComponent>().Property("_value");

            modelBuilder.Entity<TableLayoutComponent>().HasOne<HeaderLayoutComponent>("_header");
            modelBuilder.Entity<HeaderLayoutComponent>().HasOne<HorizontalStackLayoutComponent<LayoutComponent>>("_stack");
            modelBuilder.Entity<AssignmentsComponent>().HasOne<HorizontalStackLayoutComponent<AssignmentColumnComponent>>("_stack");
        }
    }
}