using System.Collections.Generic;
using SeaInk.Core.APIs;
using SeaInk.Core.Entities;

namespace Infrastructure.APIs
{
    public interface ITestUniversitySystemApi: IUniversitySystemApi
    {
        List<User> Users { get; }
        List<Student> Students { get; }
        List<Mentor> Mentors { get; }
        List<StudyGroup> Groups { get; }
        List<StudyAssignment> Assignments { get; }
        List<Subject> Subjects { get; }
        List<StudentAssignmentProgress> StudentAssignmentProgresses { get; }

        delegate void HandleLog(string message);
        event HandleLog Log;

        public int TotalCallCount { get; }

        public int GetUserCallCount { get; }
        public int GetStudentCallCount { get; }
        public int GetMentorCallCount { get; }
        public int GetStudyGroupCallCount { get; }
        public int GetStudyAssignmentCallCount { get; }
        public int GetSubjectCallCount { get; }
        public int GetStudentAssignmentProgressCallCount { get; }
        public int GetDivisionCallCount { get; }

        public int SaveUserCallCount { get; }
        public int SaveStudentCallCount { get; }
        public int SaveMentorCallCount { get; }
        public int SaveStudyGroupCallCount { get; }
        public int SaveStudyAssignmentCallCount { get; }
        public int SaveSubjectCallCount { get; }
        public int SaveStudentAssignmentProgressCallCount { get; }
        public int SaveDivisionCallCount { get; }
    }
}