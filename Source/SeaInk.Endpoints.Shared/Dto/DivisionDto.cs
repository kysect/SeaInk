using System.Collections.Generic;
using System.Linq;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public record DivisionDto(
        int Id,
        string Title,
        string SpreadsheetId,
        IReadOnlyList<StudyGroupSubjectDto> StudyGroupSubjects);

    public static class DivisionExtension
    {
        public static DivisionDto ToDto(this Division division)
        {
            return new DivisionDto(division.Id,
                                   division.Title,
                                   division.SpreadsheetId,
                                   division.StudyGroupSubjects.Select(s => s.ToDto()).ToList());
        }
    }
}