using System.Collections.Generic;

namespace SeaInk.Core.Entity
{
    public class CuratedGroup: Base.Group
    {
        public Curator Curator { get; set; }
        public string Title { get; set; }

        public Base.Program Program { get; set; }
        public Dictionary<Base.Milestone, Dictionary<int, float>> Progress { get; set; }
    }
}