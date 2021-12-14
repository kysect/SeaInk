using System;
using System.Collections.Generic;
using System.Linq;
using SeaInk.Core.Entities;

namespace SeaInk.Application.Dtos
{
    public record SubjectDto(Guid Id, int UniversityId, string Name, IReadOnlyCollection<StudyAssignmentDto> Assignments);

    public static class SubjectDtoExtension
    {
        public static SubjectDto ToDto(this Subject subject)
            => new SubjectDto(
                subject.Id,
                subject.UniversityId,
                subject.Name,
                subject.Assignments.Select(a => a.ToDto()).ToList());
    }
}