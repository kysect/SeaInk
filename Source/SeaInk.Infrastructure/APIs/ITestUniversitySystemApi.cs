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
        List<StudyAssignment> StudyAssignments { get; }
        List<Subject> Subjects { get; }
        List<StudentAssignmentProgress> StudentAssignmentProgresses { get; }
        List<Division> Divisions { get; }

        delegate void HandleLog(string message);

        event HandleLog Log;
    }
}