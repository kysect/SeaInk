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
        List<Division> Divisions { get; }

        delegate void HandleLog(string message);

        event HandleLog Log;

        int TotalCallCount { get; }
        
        int GetUserCallCount { get; }
        int GetStudentCallCount { get; }
        int GetMentorCallCount { get; }
        int GetStudyGroupCallCount { get; }
        int GetStudyAssignmentCallCount { get; }
        int GetSubjectCallCount { get; }
        int GetStudentAssignmentProgressCallCount { get; }
        int GetDivisionCallCount { get; }
        
        int SaveUserCallCount { get; }
        int SaveStudentCallCount { get; }
        int SaveMentorCallCount { get; }
        int SaveStudyGroupCallCount { get; }
        int SaveStudyAssignmentCallCount { get; }
        int SaveSubjectCallCount { get; }
        int SaveStudentAssignmentProgressCallCount { get; }
        int SaveDivisionCallCount { get; }
    }
}