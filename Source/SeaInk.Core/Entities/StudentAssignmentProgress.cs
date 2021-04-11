using SeaInk.Core.Models;

namespace SeaInk.Core.Entities
{
    public class StudentAssignmentProgress
    {
        public Student Student { get; set; }
        public StudyAssignment Assignment { get; set; }
        public AssignmentProgress Progress { get; set; }

        public StudentAssignmentProgress()
        {
            
        }
        public StudentAssignmentProgress(Student student, StudyAssignment assignment, AssignmentProgress progress)
        {
            Student = student;
            Assignment = assignment;
            Progress = progress;
        }
    }
}