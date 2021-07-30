using System;
using System.Collections.Generic;
using System.Linq;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public  List<StudyAssignmentDto> Assignments { get; set; }

        public SubjectDto()
        {
            Id = -1;
        }

        public SubjectDto(Subject subject)
        {
            Id = subject.Id;
            Title = subject.Title;
            StartDate = subject.StartDate;
            EndDate = subject.EndDate;
            Assignments = subject.Assignments.Select(assaignment => new StudyAssignmentDto(assaignment)).ToList();
        }
    }
}