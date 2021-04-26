using System;
using System.IO;

namespace SeaInk.Core.Entities
{
    public class StudyAssignment
    {
        public int SystemId { get; set; }
        public string Title { get; set; }
        public bool IsMilestone { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public float MinPoints { get; set; }
        public float MaxPoints { get; set; }

        public StudyAssignment()
        {
            SystemId = -1;
        }

        public StudyAssignment(int id, string title, bool isMilestone, DateTime start,
            DateTime end, float min, float max)
        {
            SystemId = id;
            Title = title;
            IsMilestone = isMilestone;
            StartDate = start;
            EndDate = end;
            MinPoints = min;
            MaxPoints = max;
        }
    }
}