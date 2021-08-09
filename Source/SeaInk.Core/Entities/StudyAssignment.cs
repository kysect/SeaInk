using System;
using System.ComponentModel.DataAnnotations;

namespace SeaInk.Core.Entities
{
    public class StudyAssignment: IUniversityEntity
    {
        [Key]
        public int Id { get; init; }

        public int UniversityId { get; init; }

        public string Title { get; set; }
        public bool IsMilestone { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public float MinPoints { get; set; }
        public float MaxPoints { get; set; }
    }
}