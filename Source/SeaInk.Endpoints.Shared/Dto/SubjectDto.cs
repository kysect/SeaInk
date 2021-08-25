using System;
using System.Collections.Generic;
using System.Linq;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public record SubjectDto(
        int Id,
        string Title,
        DateTime StartDate,
        DateTime EndDate,
        IReadOnlyList<StudyAssignmentDto> Assignments);

    public static class SubjectExtension
    {
        public static SubjectDto ToDto(this Subject subject)
        {
            return new SubjectDto(subject.Id, 
                                  subject.Name, 
                                  subject.StartDate, 
                                  subject.EndDate,
                                  subject.Assignments.Select(assignment => assignment.ToDto()).ToList());
        }
    }
}