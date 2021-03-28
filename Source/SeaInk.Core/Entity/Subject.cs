using System;
using System.Collections.Generic;

namespace SeaInk.Core.Entity
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public List<StudyAssignment> Assignments { get; set; }
    }
}