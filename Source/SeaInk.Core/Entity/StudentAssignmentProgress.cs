using SeaInk.Core.Model;

namespace SeaInk.Core.Entity
{
    public class StudentAssignmentProgress
    {
        public Student Student { get; set; }
        public StudyAssignment Assignment { get; set; }
        public AssignmentProgress Progress { get; set; }
    }
}