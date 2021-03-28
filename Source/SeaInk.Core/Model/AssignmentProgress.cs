using System;

namespace SeaInk.Core.Model
{
    public class AssignmentProgress
    {
        public DateTime CompletionDate;
        public double Points { get; set; }

        public AssignmentProgress(DateTime date, float points)
        {
            CompletionDate = date;
            Points = points;
        }
    }
}