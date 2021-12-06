using SeaInk.Core.Entities;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Models
{
    public class StudentAssignmentProgress
    {
        public StudentAssignmentProgress(Student student, StudyAssignment assignment, AssignmentProgress progress)
        {
            Student = student.ThrowIfNull();
            Assignment = assignment.ThrowIfNull();
            Progress = progress.ThrowIfNull();
        }

        public Student Student { get; set; }
        public StudyAssignment Assignment { get; set; }
        public AssignmentProgress Progress { get; set; }
    }
}