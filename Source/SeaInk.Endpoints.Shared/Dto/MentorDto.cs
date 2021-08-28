using System.Collections.Generic;
using System.Linq;
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
        List<StudyGroupSubjectDto>  StudyGroupSubjects);

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
                                 mentor.StudyGroupSubjects.Select(s => s.ToDto()).ToList());
        }
    }
}