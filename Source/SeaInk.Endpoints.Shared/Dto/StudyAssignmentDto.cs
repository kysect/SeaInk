using System;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public class StudyAssignmentDto
    {
        public int SystemId { get; set; }
        public string Title { get; set; }
        public bool IsMilestone { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public float MinPoints { get; set; }
        public float MaxPoints { get; set; }

        public StudyAssignmentDto()
        {
            SystemId = -1;
        }

        public StudyAssignmentDto(StudyAssignment sa)
        {
            SystemId = sa.SystemId;
            Title = sa.Title;
            IsMilestone = sa.IsMilestone;
            StartDate = sa.StartDate;
            EndDate = sa.EndDate;
            MinPoints = sa.MinPoints;
            MaxPoints = sa.MaxPoints;
        }
    }
}