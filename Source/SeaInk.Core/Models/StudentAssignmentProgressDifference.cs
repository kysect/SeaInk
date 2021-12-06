using SeaInk.Core.Entities;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Models
{
    public class StudentAssignmentProgressDifference
    {
        public StudentAssignmentProgressDifference(
            Student student, StudyAssignment assignment, AssignmentProgress? oldProgress, AssignmentProgress? newProgress)
        {
            Student = student.ThrowIfNull();
            Assignment = assignment.ThrowIfNull();
            OldProgress = oldProgress;
            NewProgress = newProgress;
        }

        public Student Student { get; }
        public StudyAssignment Assignment { get; }

        public AssignmentProgress? NewProgress { get; }
        public AssignmentProgress? OldProgress { get; }
    }
}