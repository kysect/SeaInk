using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;

namespace Infrastructure.APIs
{
    public class FakeUniversitySystemApi: ITestUniversitySystemApi
    {
        private readonly Faker<User> _userFaker;
        private readonly Faker<Student> _studentFaker;
        private readonly Faker<Mentor> _mentorFaker;
        private readonly Faker<StudyGroup> _groupFaker;
        private readonly Faker<StudyAssignment> _assignmentsFaker;
        private readonly Faker<Subject> _subjectFaker;
        private readonly Faker<StudentAssignmentProgress> _studentAssignmentProgressFaker;
        private readonly Faker<Division> _divisionFaker;

        public List<User> Users { get; } = new();
        public List<Student> Students { get; } = new();
        public List<Mentor> Mentors { get; } = new();
        public List<StudyGroup> Groups { get; } = new();
        public List<StudyAssignment> StudyAssignments { get; } = new();
        public List<Subject> Subjects { get; } = new();
        public List<StudentAssignmentProgress> StudentAssignmentProgresses { get; } = new();
        public List<Division> Divisions { get; } = new();
        public List<StudyGroupSubject> StudyGroupSubjects { get; } = new();

        public event ITestUniversitySystemApi.HandleLog Log;

        public FakeUniversitySystemApi(int mentorCount = 1)
        {
            _userFaker = new Faker<User>("ru")
                .CustomInstantiator(faker => new User
                {
                    UniversityId = faker.IndexFaker,
                    FirstName = faker.Person.FirstName,
                    MiddleName = faker.Person.UserName,
                    LastName = faker.Person.LastName
                });

            _studentFaker = new Faker<Student>("ru")
                .CustomInstantiator(faker => new Student
                {
                    UniversityId = faker.IndexFaker,
                    FirstName = faker.Person.FirstName,
                    MiddleName = faker.Person.UserName,
                    LastName = faker.Person.LastName
                });

            _mentorFaker = new Faker<Mentor>("ru")
                .CustomInstantiator(faker => new Mentor
                {
                    UniversityId = faker.IndexFaker,
                    FirstName = faker.Person.FirstName,
                    MiddleName = faker.Person.UserName,
                    LastName = faker.Person.LastName
                });

            _groupFaker = new Faker<StudyGroup>()
                .CustomInstantiator(faker => new StudyGroup
                {
                    UniversityId = faker.IndexFaker,
                    Name = faker.Lorem.Letter().ToUpper() + faker.Random.Number(1000, 9999)
                })
                .Rules((f, g) =>
                {
                    List<Student> freeStudents = Students.Where(s => s.Group is null).ToList();
                    g.Students = f.Random.ArrayElements(freeStudents.ToArray(), Math.Min(f.Random.Int(15, 25), freeStudents.Count)).ToList();
                })
                .FinishWith((_, g) =>
                {
                    foreach (Student student in g.Students)
                        student.Group = g;
                });

            _assignmentsFaker = new Faker<StudyAssignment>("ru")
                .CustomInstantiator(faker => new StudyAssignment
                {
                    UniversityId = faker.IndexFaker,
                    Title = faker.Name.JobType(),
                    IsMilestone = faker.Random.Bool(),
                    MaxPoints = faker.Random.Float(5, 10),
                    MinPoints = faker.Random.Float(0, 5)
                });

            _subjectFaker = new Faker<Subject>()
                .CustomInstantiator(faker => new Subject
                {
                    UniversityId = faker.IndexFaker,
                    Name = faker.Name.JobArea(),
                    StartDate = faker.Date.Past(),
                    EndDate = faker.Date.Future(),
                    Assignments = faker.Random.ArrayElements(StudyAssignments.ToArray(), faker.Random.Int(5, 10)).ToList()
                })
                .FinishWith((f, s) =>
                {
                    foreach (StudyAssignment assignment in s.Assignments)
                    {
                        assignment.EndDate = f.Date.Past(1, s.EndDate);
                        assignment.StartDate = f.Date.Between(s.StartDate, assignment.EndDate);
                    }
                });


            _studentAssignmentProgressFaker = new Faker<StudentAssignmentProgress>()
                .CustomInstantiator(faker => new StudentAssignmentProgress
                {
                    Assignment = faker.Random.ArrayElement(StudyAssignments.ToArray()),
                    Student = faker.Random.ArrayElement(Students.ToArray())
                })
                .RuleFor(p => p.Progress,
                         (f, p) => new AssignmentProgress
                         (
                             f.Date.Between(p.Assignment.StartDate, p.Assignment.EndDate),
                             f.Random.Double(p.Assignment.MinPoints, p.Assignment.MaxPoints)
                         ));

            Faker<StudyGroupSubject> studyGroupSubjectFaker = new Faker<StudyGroupSubject>()
                .CustomInstantiator(faker => new StudyGroupSubject
                {
                    Id = faker.IndexFaker,
                    StudyGroup = faker.Random.ArrayElement(Groups.ToArray()),
                    Subject = faker.Random.ArrayElement(Subjects.ToArray()),
                    Mentors = new List<Mentor> { faker.Random.ArrayElement(Mentors.ToArray()) }
                });


            _divisionFaker = new Faker<Division>("ru")
                .CustomInstantiator(faker => new Division
                {
                    Id = faker.IndexFaker,
                    SpreadsheetId = faker.Internet.Url(),
                    StudyGroupSubjects = studyGroupSubjectFaker.Generate(faker.Random.Int(1,4))
                })
                .FinishWith((_, d) => { d.StudyGroupSubjects.ForEach(sgs => sgs.Mentors.ForEach(m => m.StudyGroupSubjects.Add(sgs))); });

            GenerateInitialData(mentorCount);
        }

        private void GenerateInitialData(int mentorCount)
        {
            int subjectCount = mentorCount * 2;
            int divisionCount = subjectCount * 2;
            int groupCount = divisionCount * divisionCount * 6;
            int studentCount = groupCount * 20;

            int assignmentCount = subjectCount * 6;
            int studentAssignmentProgressCount = assignmentCount * 15;

            Users.AddRange(_userFaker.Generate(20));
            Students.AddRange(_studentFaker.Generate(studentCount));
            Mentors.AddRange(_mentorFaker.Generate(mentorCount));
            Groups.AddRange(_groupFaker.Generate(groupCount));
            StudyAssignments.AddRange(_assignmentsFaker.Generate(assignmentCount));
            Subjects.AddRange(_subjectFaker.Generate(subjectCount));
            StudentAssignmentProgresses.AddRange(_studentAssignmentProgressFaker.Generate(studentAssignmentProgressCount));
            Divisions.AddRange(_divisionFaker.Generate(divisionCount));
            StudyGroupSubjects.AddRange(Divisions.SelectMany(d => d.StudyGroupSubjects));
        }

        public User GetUser(int id)
        {
            Log?.Invoke("Got user");
            return Users.SingleOrDefault(u => u.UniversityId == id);
        }

        public Student GetStudent(int id)
        {
            Log?.Invoke("Got student");
            return Students.SingleOrDefault(x => x.UniversityId == id);
        }

        public Mentor GetMentor(int id)
        {
            Log?.Invoke("Got mentor");
            return Mentors.SingleOrDefault(x => x.UniversityId == id);
        }

        public StudyGroup GetStudyGroup(int id)
        {
            Log?.Invoke("Got study group");
            return Groups.SingleOrDefault(x => x.UniversityId == id);
        }

        public StudyAssignment GetStudyAssignment(int id)
        {
            Log?.Invoke("Got study assignment");
            return StudyAssignments.SingleOrDefault(x => x.UniversityId == id);
        }

        public Subject GetSubject(int id)
        {
            Log?.Invoke("Got subject");
            return Subjects.SingleOrDefault(x => x.UniversityId == id);
        }

        public StudentAssignmentProgress GetStudentAssignmentProgress(int studentId, int assignmentId)
        {
            Log?.Invoke("Got student assignment progress");
            return StudentAssignmentProgresses
                .SingleOrDefault(p => p.Student.UniversityId == studentId && p.Assignment.UniversityId == assignmentId);
        }

        public Division GetDivision(int mentorId, int subjectId)
        {
            Log?.Invoke("Got division");
            return Divisions
                .SingleOrDefault(d => d.StudyGroupSubjects
                    .Where(sgs => sgs.Subject.Id == subjectId)
                    .Any(sgs => sgs.Mentors.Any(m => m.Id == mentorId)));
        }

        public List<StudyGroupSubject> GetStudyGroupSubjects(int mentorId, int subjectId)
        {
            return StudyGroupSubjects
                .Where(sgs => sgs.Subject.Id == subjectId)
                .Where(sgs => sgs.Mentors.Any(m => m.Id == mentorId))
                .ToList();
        }

        public void SaveUser(User user)
        {
            Log?.Invoke($"Saved {nameof(user)}");
        }

        public void SaveStudent(Student student)
        {
            Log?.Invoke($"Saved {nameof(student)}");
        }

        public void SaveMentor(Mentor mentor)
        {
            Log?.Invoke($"Saved {nameof(mentor)}");
        }

        public void SaveStudyGroup(StudyGroup group)
        {
            Log?.Invoke($"Saved {nameof(group)}");
        }

        public void SaveSubject(Subject subject)
        {
            Log?.Invoke($"Saved {nameof(subject)}");
        }

        public void SaveDivision(Division division)
        {
            Log?.Invoke($"Saved {nameof(division)}");
        }

        public void SaveStudyAssignment(StudyAssignment assignment)
        {
            Log?.Invoke($"Saved {nameof(assignment)}");
        }

        public void SaveAssignmentProgress(StudentAssignmentProgress progress)
        {
            Log?.Invoke($"Saved {nameof(progress)}");
        }
    }
}
