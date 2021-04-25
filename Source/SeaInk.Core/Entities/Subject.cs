using System;
using System.Collections.Generic;

namespace SeaInk.Core.Entities
{
    public class Subject
    {
        public int Id { get; set; } = -1;
        public string Title { get; set; }  = "";

        public DateTime StartDate { get; set; } = new DateTime();
        public DateTime EndDate { get; set; } = new DateTime();

        public List<StudyAssignment> Assignments { get; set; } = new List<StudyAssignment>();

        public Subject()
        {
            
        }
        public Subject(int id, string title, DateTime startDate, DateTime endDate, List<StudyAssignment>? assignments = null)
        {
            Id = id;
            Title = title;
            StartDate = startDate;
            EndDate = endDate;

            Assignments = assignments ?? new List<StudyAssignment>();
        }
    }
}