using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Bogus;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;

namespace Infrastructure.APIs
{
    public class FakeUniversitySystemApi: ITestUniversitySystemApi

    {
        private int _currentId;

        private readonly Faker<User> _userFaker;
        private readonly Faker<Student> _studentFaker;
        private readonly Faker<Mentor> _mentorFaker;
        private readonly Faker<StudyGroup> _groupFaker;
        private readonly Faker<StudyAssignment> _assignmentsFaker;
        private readonly Faker<Subject> _subjectFaker;
        private readonly Faker<StudentAssignmentProgress> _studentAssignmentProgressFaker;
        private readonly Faker<Division> _divisionFaker;

        private int _totalCallCount;

        private int _getUserCallCount;
        private int _getStudentCallCount;
        private int _getMentorCallCount;
        private int _getStudyGroupCallCount;
        private int _getStudyAssignmentCallCount;
        private int _getSubjectCallCount;
        private int _getStudentAssignmentProgressCallCount;
        private int _getDivisionCallCount;

        private int _saveUserCallCount;
        private int _saveStudentCallCount;
        private int _saveMentorCallCount;
        private int _saveStudyGroupCallCount;
        private int _saveStudyAssignmentCallCount;
        private int _saveSubjectCallCount;
        private int _saveStudentAssignmentProgressCallCount;
        private int _saveDivisionCallCount;

        public List<User> Users { get; } = new();
        public List<Student> Students { get; } = new();
        public List<Mentor> Mentors { get; } = new();
        public List<StudyGroup> Groups { get; } = new();
        public List<StudyAssignment> Assignments { get; } = new();
        public List<Subject> Subjects { get; } = new();
        public List<StudentAssignmentProgress> StudentAssignmentProgresses { get; } = new();
        public List<Division> Divisions { get; } = new();

        public int TotalCallCount => _totalCallCount;

        public int GetUserCallCount => _getUserCallCount;
        public int GetStudentCallCount => _getStudentCallCount;
        public int GetMentorCallCount => _getMentorCallCount;
        public int GetStudyGroupCallCount => _getStudyGroupCallCount;
        public int GetStudyAssignmentCallCount => _getStudyAssignmentCallCount;
        public int GetSubjectCallCount => _getSubjectCallCount;
        public int GetStudentAssignmentProgressCallCount => _getStudentAssignmentProgressCallCount;
        public int GetDivisionCallCount => _getDivisionCallCount;

        public int SaveUserCallCount => _saveUserCallCount;
        public int SaveStudentCallCount => _saveStudentCallCount;
        public int SaveMentorCallCount => _saveMentorCallCount;
        public int SaveStudyGroupCallCount => _saveStudyGroupCallCount;
        public int SaveStudyAssignmentCallCount => _saveStudyAssignmentCallCount;
        public int SaveSubjectCallCount => _saveSubjectCallCount;
        public int SaveStudentAssignmentProgressCallCount => _saveStudentAssignmentProgressCallCount;
        public int SaveDivisionCallCount => _saveDivisionCallCount;

        public FakeUniversitySystemApi()
        {
            _userFaker = new Faker<User>("ru")
                .CustomInstantiator(faker => new User
                {
                    UniversityId = Interlocked.Increment(ref _currentId),
                    FirstName = faker.Person.FirstName,
                    MidName = faker.Person.UserName,
                    LastName = faker.Person.LastName
                });

            _studentFaker = new Faker<Student>("ru")
                .CustomInstantiator(faker => new Student
                {
                    UniversityId = Interlocked.Increment(ref _currentId),
                    FirstName = faker.Person.FirstName,
                    MidName = faker.Person.UserName,
                    LastName = faker.Person.LastName
                });

            _mentorFaker = new Faker<Mentor>("ru")
                .CustomInstantiator(faker => new Mentor
                {
                    UniversityId = Interlocked.Increment(ref _currentId),
                    FirstName = faker.Person.FirstName,
                    MidName = faker.Person.UserName,
                    LastName = faker.Person.LastName
                });

            _groupFaker = new Faker<StudyGroup>()
                .CustomInstantiator(faker => new StudyGroup
                {
                    UniversityId = Interlocked.Increment(ref _currentId),
                    Name = faker.Lorem.Letter() + faker.Random.Number(1000, 9999),
                    Students = faker.Random.ArrayElements(Students.Where(s => s.Group is null).ToArray(),
                                                          faker.Random.Int(15, 25)).ToList()
                })
                .RuleFor(g => g.Admin, (f, g) => f.Random.ArrayElement(g.Students.ToArray()))
                .FinishWith((_, g) =>
                {
                    foreach (Student student in g.Students)
                        student.Group = g;
                });

            _assignmentsFaker = new Faker<StudyAssignment>("ru")
                .CustomInstantiator(faker => new StudyAssignment
                {
                    UniversityId = Interlocked.Increment(ref _currentId),
                    Title = faker.Name.JobType(),
                    IsMilestone = faker.Random.Bool(),
                    MaxPoints = faker.Random.Float(5, 10),
                    MinPoints = faker.Random.Float(0, 5)
                });

            _subjectFaker = new Faker<Subject>("ru")
                .CustomInstantiator(faker => new Subject
                {
                    UniversityId = Interlocked.Increment(ref _currentId),
                    Title = faker.Name.JobArea(),
                    StartDate = faker.Date.Past(),
                    EndDate = faker.Date.Future(),
                    Assignments = faker.Random.ArrayElements(Assignments.ToArray(), faker.Random.Int(5, 10)).ToList()
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
                    Assignment = faker.Random.ArrayElement(Assignments.ToArray()),
                    Student = faker.Random.ArrayElement(Students.ToArray())
                })
                .RuleFor(p => p.Progress,
                         (f, p) => new AssignmentProgress
                         (
                             f.Date.Between(p.Assignment.StartDate, p.Assignment.EndDate),
                             f.Random.Float(p.Assignment.MinPoints, p.Assignment.MaxPoints)
                         ));

            _divisionFaker = new Faker<Division>()
                .CustomInstantiator(faker => new Division
                {
                    SpreadsheetId = faker.Internet.Url(),
                    Subject = faker.Random.ArrayElement(Subjects.ToArray()),
                    Mentor = faker.Random.ArrayElement(Mentors.ToArray()),
                    Groups = faker.Random.ArrayElements(Groups.ToArray(), faker.Random.Int(1, 4)).ToList()
                })
                .FinishWith((_, d) => { d.Mentor.Divisions.Add(d); });

            GenerateInitialData();
        }

        private void GenerateInitialData()
        {
            Users.AddRange(_userFaker.Generate(50));
            Students.AddRange(_studentFaker.Generate(200));
            Mentors.AddRange(_mentorFaker.Generate(5));
            Groups.AddRange(_groupFaker.Generate(15));
            Assignments.AddRange(_assignmentsFaker.Generate(50));
            Subjects.AddRange(_subjectFaker.Generate(5));
            StudentAssignmentProgresses.AddRange(_studentAssignmentProgressFaker.Generate(200));
            Divisions.AddRange(_divisionFaker.Generate(5));
        }

        public User GetUser(int id)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getUserCallCount);
            return Users.SingleOrDefault(u => u.UniversityId == id);
        }

        public Student GetStudent(int id)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getStudentCallCount);
            return Students.SingleOrDefault(x => x.UniversityId == id);
        }

        public Mentor GetMentor(int id)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getMentorCallCount);
            return Mentors.SingleOrDefault(x => x.UniversityId == id);
        }

        public StudyGroup GetStudyGroup(int id)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getStudyGroupCallCount);
            return Groups.SingleOrDefault(x => x.UniversityId == id);
        }

        public StudyAssignment GetStudyAssignment(int id)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getStudyAssignmentCallCount);
            return Assignments.SingleOrDefault(x => x.UniversityId == id);
        }

        public Subject GetSubject(int id)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getSubjectCallCount);
            return Subjects.SingleOrDefault(x => x.UniversityId == id);
        }

        public StudentAssignmentProgress GetStudentAssignmentProgress(int studentId, int assignmentId)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getStudentAssignmentProgressCallCount);
            return StudentAssignmentProgresses
                .SingleOrDefault(p => p.Student.UniversityId == studentId && p.Assignment.UniversityId == assignmentId);
        }

        public Division GetDivision(int mentorId, int subjectId)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getDivisionCallCount);
            return Divisions.SingleOrDefault(d => d.Mentor.UniversityId == mentorId && d.Subject.UniversityId == subjectId);
        }

        public void SaveUser(User user)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _saveUserCallCount);
            Log?.Invoke($"Saved {nameof(user)}");
        }

        public void SaveStudent(Student student)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _saveStudentCallCount);
            Log?.Invoke($"Saved {nameof(student)}");
        }

        public void SaveMentor(Mentor mentor)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _saveMentorCallCount);
            Log?.Invoke($"Saved {nameof(mentor)}");
        }

        public void SaveStudyGroup(StudyGroup group)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _saveStudyGroupCallCount);
            Log?.Invoke($"Saved {nameof(group)}");
        }

        public void SaveSubject(Subject subject)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _saveSubjectCallCount);
            Log?.Invoke($"Saved {nameof(subject)}");
        }

        public void SaveDivision(Division division)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _saveDivisionCallCount);
            Log?.Invoke($"Saved {nameof(division)}");
        }

        public void SaveStudyAssignment(StudyAssignment assignment)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _saveStudyAssignmentCallCount);
            Log?.Invoke($"Saved {nameof(assignment)}");
        }

        public void SaveAssignmentProgress(StudentAssignmentProgress progress)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _saveStudentAssignmentProgressCallCount);
            Log?.Invoke($"Saved {nameof(progress)}");
        }

        public delegate void LogHandler(string message);

        public event LogHandler Log;
    }
}