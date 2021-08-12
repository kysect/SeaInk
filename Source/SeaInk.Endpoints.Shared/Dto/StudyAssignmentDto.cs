using System;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public class StudyAssignmentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsMilestone { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public float MinPoints { get; set; }
        public float MaxPoints { get; set; }

        public StudyAssignmentDto()
        {
            Id = -1;
        }

        public StudyAssignmentDto(StudyAssignment sa)
        {
            Id = sa.Id;
            Title = sa.Title;
            IsMilestone = sa.IsMilestone;
            StartDate = sa.StartDate;
            EndDate = sa.EndDate;
            MinPoints = sa.MinPoints;
            MaxPoints = sa.MaxPoints;
        }
    }
}