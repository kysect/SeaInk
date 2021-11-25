using System.Collections.Generic;
using SeaInk.Application.UniversityEntityModels;

namespace SeaInk.Application.Services
{
    public interface IUniversitySystemService
    {
        UniversityUserModel GetMentor(int mentorUniversityId);
        IReadOnlyCollection<UniversitySubjectModel> GetMentorSubjects(int mentorUniversityId);
        IReadOnlyCollection<UniversityStudyGroupModel> GetMentorSubjectGroups(int mentorUniversityId, int subjectUniversityId);

        IReadOnlyCollection<UniversityUserModel> GetGroupStudents(int groupUniversityId);

        IReadOnlyCollection<UniversityStudyAssignmentModel> GetSubjectAssignments(int subjectUniversityId);
    }
}