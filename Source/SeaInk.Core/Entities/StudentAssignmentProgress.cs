using SeaInk.Core.Models;

namespace SeaInk.Core.Entities
{
    public class StudentAssignmentProgress
    {
        public Student Student { get; set; }
        public StudyAssignment Assignment { get; set; }
        public AssignmentProgress Progress { get; set; }
    }
}