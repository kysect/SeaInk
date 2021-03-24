using System;

namespace SeaInk.Core.Models
{
    public class AssignmentProgress
    {
        public DateTime CompletionDate;
        public float Points { get; set; }

        public AssignmentProgress(DateTime date, float points)
        {
            CompletionDate = date;
            Points = points;
        }
    }
}