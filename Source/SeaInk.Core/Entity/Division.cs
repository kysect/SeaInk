using System.Collections.Generic;

namespace SeaInk.Core.Entity
{
    public class Division
    {
        public Subject Subject { get; set; }
        public List<StudyGroup> Groups { get; set; }
    }
}