using System;
using System.Collections.Generic;

namespace SeaInk.Core.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<StudyAssignment> Assignments { get; set; }

        public Subject()
        {
            Id = -1;
            Assignments = new List<StudyAssignment>();
        }

        public Subject(int id, string title, DateTime startDate, DateTime endDate,
            List<StudyAssignment> assignments = null)
        {
            Id = id;
            Title = title;
            StartDate = startDate;
            EndDate = endDate;

            Assignments = assignments ?? new List<StudyAssignment>();
        }
    }
}