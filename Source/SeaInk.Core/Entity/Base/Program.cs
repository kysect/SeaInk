using System;
using System.Collections.Generic;

namespace SeaInk.Core.Entity.Base
{
    public class Program
    {
        public string Title { get; set; }
        
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        public List<Milestone> Milestones;
    }
}