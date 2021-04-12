using System;
using System.Linq;
using Bogus;
using SeaInk.Core.Entities;
using SeaInk.Core.Models.Tables;

namespace SeaInk.Core
{
    public class FakeUniversitySystemApi : IUniversitySystemApi
    {
        public UniversitySystemUser GetUserBySystemId(int userId)
        {
            return UniversitySystemUserFaker.RuleFor(u => u.SystemId, userId).Generate();
        }

        public Student GetStudentBySystemId(int studentId)
        {
            return StudentFaker.RuleFor(s => s.SystemId, studentId).Generate();
        }

        public Mentor GetMentorBySystemId(int mentorId)
        {
            return MentorFaker.RuleFor(m => m.SystemId, mentorId).Generate();
        }

        public StudyGroup GetStudyGroupBySystemId(int groupId)
        {
            return StudyGroupFaker.RuleFor(g => g.SystemId, groupId).Generate();
        }

        public StudyGroup GetStudyGroupByStudentSystemId(int studentId)
        {
            return StudyGroupFaker.Rules((_, group) =>
            {
                group.Students.Add(StudentFaker.RuleFor(s => s.SystemId, studentId));
            }).Generate();
        }

        public Subject GetSubjectBySystemId(int subjectId)
        {
            return SubjectFaker.RuleFor(s => s.Id, subjectId).Generate();
        }

        public StudentAssignmentProgress GetStudentAssignmentProgressByIds(int studentId, int assignmentId)
        {
            return StudentAssignmentProgressFaker.Rules((_, progress) =>
            {
                progress.Student.SystemId = studentId;
                progress.Assignment.SystemId = assignmentId;
            });
        }

        public void SaveUser(UniversitySystemUser user)
        {
            Console.WriteLine("User saved");
        }

        public void SaveStudent(Student student)
        {
            Console.WriteLine("Student saved");
        }

        public void SaveMentor(Mentor mentor)
        {
            Console.WriteLine("Mentor saved");
        }

        public void SaveStudyGroup(StudyGroup group)
        {
            Console.WriteLine("Group saved");
        }

        public void SaveSubject(Subject subject)
        {
            Console.WriteLine("Subject saved");
        }

        public void SaveDivision(Division division)
        {
            Console.WriteLine("Division saved");
        }

        public void SaveAssignmentProgress(StudentAssignmentProgress progress)
        {
            Console.WriteLine("Progress saved");
        }

        /* Faker */
        /* Fake Entities */
        private static Faker<Division> DivisionFaker { get; }

        private static Faker<Mentor> MentorFaker { get; }

        private static Faker<Student> StudentFaker { get; }

        private static Faker<StudentAssignmentProgress> StudentAssignmentProgressFaker { get; }

        private static Faker<StudyAssignment> StudyAssignmentFaker { get; }

        private static Faker<StudyGroup> StudyGroupFaker { get; }

        private static Faker<Subject> SubjectFaker { get; }

        private static Faker<UniversitySystemUser> UniversitySystemUserFaker { get; }


        /* Models */
        private static Faker<AssignmentProgress> AssignmentProgressFaker { get; }

        static FakeUniversitySystemApi()
        {
            StudyAssignmentFaker = new Faker<StudyAssignment>()
                .Rules((f, a) =>
                {
                    a.SystemId = f.IndexFaker;
                    a.Title = f.Commerce.Department();
                    a.IsMilestone = f.Random.Bool();
                    a.StartDate = f.Date.Past();
                    a.EndDate = f.Date.Future();
                    a.MinPoints = f.Random.Float(0, 5);
                    a.MaxPoints = f.Random.Float(5, 10);
                });
            UniversitySystemUserFaker = new Faker<UniversitySystemUser>()
                .Rules((f, u) =>
                {
                    u.SystemId = f.IndexFaker;
                    u.Token = f.Internet.Password();
                    u.FirstName = f.Person.FirstName;
                    u.LastName = f.Person.LastName;
                    u.MidName = f.Person.UserName;
                });
            AssignmentProgressFaker = new Faker<AssignmentProgress>()
                .Rules((f, p) =>
                {
                    p.CompletionDate = f.Date.Past();
                    p.Points = f.Random.Float(5, 10);
                });

            StudentFaker = new Faker<Student>()
                .Rules((f, s) =>
                {
                    s.SystemId = f.IndexFaker;
                    s.Token = f.Internet.Password();
                    s.FirstName = f.Person.FirstName;
                    s.LastName = f.Person.LastName;
                    s.MidName = f.Person.UserName;
                });

            StudyGroupFaker = new Faker<StudyGroup>()
                .Rules((f, g) =>
                {
                    g.SystemId = f.IndexFaker;
                    g.Name = f.Address.BuildingNumber();
                    g.Students = StudentFaker
                        .RuleFor(s => s.Group, g)
                        .Generate(f.Random.Int(15, 25))
                        .ToList();
                    g.Admin = g.Students[0];
                });

            SubjectFaker = new Faker<Subject>()
                .Rules((f, s) =>
                {
                    s.Id = f.IndexFaker;
                    s.Title = f.Commerce.Department();
                    s.StartDate = f.Date.Past();
                    s.EndDate = f.Date.Future();

                    int top = f.Random.Int(3, 5);
                    for (int i = 0; i < top; ++i)
                    {
                        s.Assignments.Add(StudyAssignmentFaker.Generate());
                    }
                });

            DivisionFaker = new Faker<Division>()
                .Rules((f, d) =>
                {
                    d.Subject = SubjectFaker.Generate();
                    d.Groups = StudyGroupFaker.Generate(f.Random.Int(3, 5));
                });

            MentorFaker = new Faker<Mentor>()
                .Rules((f, m) =>
                {
                    m.SystemId = f.IndexFaker;
                    m.Token = f.Internet.Password();
                    m.FirstName = f.Person.FirstName;
                    m.LastName = f.Person.LastName;
                    m.MidName = f.Person.UserName;

                    m.Divisions = DivisionFaker.Generate(f.Random.Int(2, 4));
                });

            StudentAssignmentProgressFaker = new Faker<StudentAssignmentProgress>()
                .Rules((_, progress) =>
                {
                    progress.Student = StudentFaker.Generate();
                    progress.Assignment = StudyAssignmentFaker.Generate();
                    progress.Progress = AssignmentProgressFaker.Generate();
                });
        }
    }
}