using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public record MentorDto(
        int Id,
        int UniversityId,
        string FirstName,
        string LastName,
        string MiddleName,
        string FullName,
        IReadOnlyList<SubjectDto> Subjects,
        IReadOnlyList<DivisionDto> Divisions);

    public static class MentorExtension
    {
        public static MentorDto ToDto(this Mentor mentor)
        {
            return new MentorDto(mentor.Id,
                                 mentor.UniversityId,
                                 mentor.FirstName,
                                 mentor.LastName,
                                 mentor.MiddleName,
                                 mentor.FullName,
                                 mentor.Divisions
                                     .Select(d => d.Subject.ToDto())
                                     .DistinctBy(s => s.Id)
                                     .ToList(),
                                 mentor.Divisions
                                     .Select(d => d.ToDto())
                                     .ToList());
        }
    }
}