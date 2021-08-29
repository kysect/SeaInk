using System;
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
        private int _currentUserId = -1;
        private int _currentStudentId = -1;
        private int _currentMentorId = -1;
        private int _currentGroupId = -1;
        private int _currentAssignmentId = -1;
        private int _currentSubjectId = -1;

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

        public int SaveUserCallCount => _saveUserCallCount;
        public int SaveStudentCallCount => _saveStudentCallCount;
        public int SaveMentorCallCount => _saveMentorCallCount;
        public int SaveStudyGroupCallCount => _saveStudyGroupCallCount;
        public int SaveStudyAssignmentCallCount => _saveStudyAssignmentCallCount;
        public int SaveSubjectCallCount => _saveSubjectCallCount;
        public int SaveStudentAssignmentProgressCallCount => _saveStudentAssignmentProgressCallCount;
        public int SaveDivisionCallCount => _saveDivisionCallCount;

        public event ITestUniversitySystemApi.HandleLog Log;

        public FakeUniversitySystemApi(int mentorCount = 1)
        {
            _userFaker = new Faker<User>("ru")
                .CustomInstantiator(faker => new User
                {
                    UniversityId = Interlocked.Increment(ref _currentUserId),
                    FirstName = faker.Person.FirstName,
                    MiddleName = faker.Person.UserName,
                    LastName = faker.Person.LastName
                });

            _studentFaker = new Faker<Student>("ru")
                .CustomInstantiator(faker => new Student
                {
                    UniversityId = Interlocked.Increment(ref _currentStudentId),
                    FirstName = faker.Person.FirstName,
                    MiddleName = faker.Person.UserName,
                    LastName = faker.Person.LastName
                });

            _mentorFaker = new Faker<Mentor>("ru")
                .CustomInstantiator(faker => new Mentor
                {
                    UniversityId = Interlocked.Increment(ref _currentMentorId),
                    FirstName = faker.Person.FirstName,
                    MiddleName = faker.Person.UserName,
                    LastName = faker.Person.LastName
                });

            _groupFaker = new Faker<StudyGroup>()
                .CustomInstantiator(faker => new StudyGroup
                {
                    UniversityId = Interlocked.Increment(ref _currentGroupId),
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
                    UniversityId = Interlocked.Increment(ref _currentAssignmentId),
                    Title = faker.Name.JobType(),
                    IsMilestone = faker.Random.Bool(),
                    MaxPoints = faker.Random.Float(5, 10),
                    MinPoints = faker.Random.Float(0, 5)
                });

            _subjectFaker = new Faker<Subject>()
                .CustomInstantiator(faker => new Subject
                {
                    UniversityId = Interlocked.Increment(ref _currentSubjectId),
                    Name = faker.Name.JobArea(),
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
                             f.Random.Double(p.Assignment.MinPoints, p.Assignment.MaxPoints)
                         ));

            _divisionFaker = new Faker<Division>("ru")
                .CustomInstantiator(faker => new Division
                {
                    SpreadsheetId = faker.Internet.Url(),
                    StudyGroupSubjects = faker.Random
                        .ArrayElements(Groups.ToArray(), faker.Random.Int(1, 4))
                        .Select(group => new StudyGroupSubject
                        {
                            Id = faker.IndexFaker++,
                            StudyGroup = group,
                            Subject = faker.Random.ArrayElement(Subjects.ToArray()),
                            Mentors = new List<Mentor>() { faker.Random.ArrayElement(Mentors.ToArray()) }
                        })
                        .ToList()
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
            Assignments.AddRange(_assignmentsFaker.Generate(assignmentCount));
            Subjects.AddRange(_subjectFaker.Generate(subjectCount));
            StudentAssignmentProgresses.AddRange(_studentAssignmentProgressFaker.Generate(studentAssignmentProgressCount));
            Divisions.AddRange(_divisionFaker.Generate(divisionCount));
        }

        public User GetUser(int id)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getUserCallCount);
            Log?.Invoke("Got user");
            return Users.SingleOrDefault(u => u.UniversityId == id);
        }

        public Student GetStudent(int id)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getStudentCallCount);
            Log?.Invoke("Got student");
            return Students.SingleOrDefault(x => x.UniversityId == id);
        }

        public Mentor GetMentor(int id)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getMentorCallCount);
            Log?.Invoke("Got mentor");
            return Mentors.SingleOrDefault(x => x.UniversityId == id);
        }

        public StudyGroup GetStudyGroup(int id)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getStudyGroupCallCount);
            Log?.Invoke("Got study group");
            return Groups.SingleOrDefault(x => x.UniversityId == id);
        }

        public StudyAssignment GetStudyAssignment(int id)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getStudyAssignmentCallCount);
            Log?.Invoke("Got study assignment");
            return Assignments.SingleOrDefault(x => x.UniversityId == id);
        }

        public Subject GetSubject(int id)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getSubjectCallCount);
            Log?.Invoke("Got subject");
            return Subjects.SingleOrDefault(x => x.UniversityId == id);
        }

        public StudentAssignmentProgress GetStudentAssignmentProgress(int studentId, int assignmentId)
        {
            Interlocked.Increment(ref _totalCallCount);
            Interlocked.Increment(ref _getStudentAssignmentProgressCallCount);
            Log?.Invoke("Got student assignment progress");
            return StudentAssignmentProgresses
                .SingleOrDefault(p => p.Student.UniversityId == studentId && p.Assignment.UniversityId == assignmentId);
        }

        //TODO: rework implementation
        public StudyGroupSubject GetStudyGroupSubject(int mentorId, int subjectId)
        {
            throw new NotImplementedException();
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
    }
}
