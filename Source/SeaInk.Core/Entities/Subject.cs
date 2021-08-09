using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeaInk.Core.Entities
{
    public class Subject: IUniversityEntity
    {
        [Key]
        public int Id { get; init; }

        public int UniversityId { get; init; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual List<StudyAssignment> Assignments { get; set; } = new();
    }
}