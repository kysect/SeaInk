using System.Collections.Generic;
using SeaInk.Core.Entity.Base;

namespace SeaInk.Core.Entity
{
    public class CuratedGroup: Base.Group
    {
        public Curator Curator { get; set; }
        public new List<Student> Members { get; set; }
        public string Title { get; set; }

        public Program Program { get; set; }
        public Dictionary<Milestone, Dictionary<int, float>> Progress { get; set; }
    }
}