using System;

namespace SeaInk.Core.Entity.Base
{
    public class Milestone
    {
        public string Title { get; set; }
        
        public float Minimum { get; }
        public float Maximum { get; }

        //TODO: Change to format used for time
        public int Begin { get; set; }
        public int End { get; set; }

        public void SetScoring(float minimum, float maximum)
        {
            throw new NotImplementedException();
        }
    }
}