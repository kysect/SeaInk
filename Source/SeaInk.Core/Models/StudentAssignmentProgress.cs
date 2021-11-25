using SeaInk.Core.Entities;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Models
{
    public class StudentAssignmentProgress
    {
        public StudentAssignmentProgress(Student student, StudyAssignment assignment, AssignmentProgress progress)
        {
            Student = student.ThrowIfNull(nameof(student));
            Assignment = assignment.ThrowIfNull(nameof(assignment));
            Progress = progress.ThrowIfNull(nameof(progress));
        }

        public Student Student { get; set; }
        public StudyAssignment Assignment { get; set; }
        public AssignmentProgress Progress { get; set; }
    }
}