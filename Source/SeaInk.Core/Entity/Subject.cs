using System;
using System.Collections.Generic;

namespace SeaInk.Core.Entity
{
    public class Subject
    {
        public string Title { get; set; }
        public int SystemId { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        
        public List<int> AssignmentIds { get; set; }
    }
}