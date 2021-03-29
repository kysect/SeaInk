using System.Collections.Generic;

namespace SeaInk.Core.Entities
{
    public class Division
    {
        public Subject Subject { get; set; }
        public List<StudyGroup> Groups { get; set; }
    }
}