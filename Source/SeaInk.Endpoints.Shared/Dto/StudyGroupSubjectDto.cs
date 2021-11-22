using System.Collections.Generic;
using System.Linq;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public record StudyGroupSubjectDto(
        int Id,
        int SheetId,
        StudyGroupDto StudyGroup,
        SubjectDto Subject,
        List<int> MentorIds);

    public static class StudyGroupSubjectExtension
    {
        public static StudyGroupSubjectDto ToDto(this StudyGroupSubject studyGroupSubject)
        {
            return new StudyGroupSubjectDto(
                studyGroupSubject.Id,
                studyGroupSubject.SheetId,
                studyGroupSubject.StudyGroup.ToDto(),
                studyGroupSubject.Subject.ToDto(),
                studyGroupSubject.Mentors.Select(m => m.Id).ToList());
        }
    }
}
