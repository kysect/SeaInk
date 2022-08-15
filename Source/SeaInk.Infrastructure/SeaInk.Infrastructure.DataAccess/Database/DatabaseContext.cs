using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SeaInk.Core.Entities;
using SeaInk.Core.TableLayout;
using SeaInk.Infrastructure.DataAccess.Exceptions;
using SeaInk.Infrastructure.DataAccess.Models;
using SeaInk.Infrastructure.DataAccess.Serialization;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.DataAccess.Database
{
    public sealed class DatabaseContext : DbContext
    {
        private readonly LayoutComponentSerializationConfiguration _layoutComponentSerializationConfiguration;

        public DatabaseContext(DbContextOptions options, LayoutComponentSerializationConfiguration layoutComponentSerializationConfiguration)
            : base(options)
        {
            _layoutComponentSerializationConfiguration = layoutComponentSerializationConfiguration;
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

            modelBuilder.Entity<StudyGroupSubjectLayout>()
                .Property(ssg => ssg.Layout)
                .HasConversion(
                    c => JsonConvert
                        .SerializeObject(c, _layoutComponentSerializationConfiguration.SerializationSettings),
                    s => JsonConvert
                        .DeserializeObject<TableLayoutComponent>(s, _layoutComponentSerializationConfiguration.DeserializationSettings)
                        .ThrowIfNull(new FailedTableLayoutComponentDeserializationException(s)));
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
    }
}