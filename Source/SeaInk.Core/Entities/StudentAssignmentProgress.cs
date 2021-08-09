using System.ComponentModel.DataAnnotations;
using SeaInk.Core.Models;

namespace SeaInk.Core.Entities
{
    public class StudentAssignmentProgress: IEntity
    {
        [Key]
        public int Id { get; }

        public virtual Student Student { get; set; }
        public virtual StudyAssignment Assignment { get; set; }
        public virtual AssignmentProgress Progress { get; set; }
    }
}