using System;

namespace SeaInk.Core.Entity.Base
{
    public class Milestone
    {
        public string Title { get; set; }
        
        public float Minimum { get; }
        public float Maximum { get; }

        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        public void SetScoring(float minimum, float maximum)
        {
            throw new NotImplementedException();
        }
    }
}