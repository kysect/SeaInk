using System.Collections.Generic;
using System.Linq;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public record DivisionDto(
        int Id,
        string Title,
        string SpreadsheetId,
        int MentorId,
        string MentorName,
        SubjectDto Subject,
        IReadOnlyList<StudyGroupDto> Groups);

    public static class DivisionExtension
    {
        public static DivisionDto ToDto(this Division division)
        {
            return new DivisionDto(division.Id, division.Title, division.SpreadsheetId, division.Mentor.Id, division.Mentor.FullName,
                                   division.Subject.ToDto(), division.Groups.Select(g => g.ToDto()).ToList());
        }
    }
}