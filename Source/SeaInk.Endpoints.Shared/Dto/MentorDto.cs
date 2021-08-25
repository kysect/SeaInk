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
        //SubjectId -> Subject
        IReadOnlyDictionary<int, SubjectDto> Subjects,
        //SubjectId -> Divisions
        IReadOnlyDictionary<int, IReadOnlyList<DivisionDto>> Divisions);

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
                                     .Select(d => d.Subject)
                                     .DistinctBy(s => s.Id)
                                     .ToDictionary(s => s.Id, s => s.ToDto()),
                                 mentor.Divisions
                                     .GroupBy(d => d.Subject)
                                     .ToDictionary(g => g.Key.Id,
                                                   g => g.Select(d => d.ToDto())
                                                            .ToList() as IReadOnlyList<DivisionDto>));
        }
    }
}