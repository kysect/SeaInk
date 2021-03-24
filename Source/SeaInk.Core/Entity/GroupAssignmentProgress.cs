using System.Collections.Generic;
using SeaInk.Core.Models;

namespace SeaInk.Core.Entity
{
    public class GroupAssignmentProgress
    {
        public StudyAssignment Assignment { get; set; }

        public Dictionary<int, AssignmentProgress> Progress { get; set; }
    }
}