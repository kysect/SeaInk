using System.Collections.Generic;

namespace SeaInk.Core.Entity
{
    public class Division
    {
        public int SystemId { get; set; }
        
        public Mentor Mentor { get; set; }
        public Subject Subject { get; set; }
        public List<StudyGroup> Groups { get; set; }
    }
}