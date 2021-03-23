using System.Collections.Generic;

namespace SeaInk.Core.Entity.Base
{
    public class Program
    {
        public string Title { get; set; }
        
        //TODO: Change to format used for time
        public int Begin { get; set; }
        public int End { get; set; }

        public HashSet<Milestone> Milestones;
    }
}