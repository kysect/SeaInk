using System.Collections.Generic;
using System.Threading.Tasks;
using SeaInk.Application.Models;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;

namespace SeaInk.Application.Services
{
    public interface IUniversityService
    {
        Task<Mentor> GetMentorAsync(int universityId);

        Task<IReadOnlyCollection<SubjectModel>> GetMentorSubjectsAsync(Mentor mentor);

        Task<IReadOnlyCollection<StudyGroupModel>> GetMentorSubjectGroupsAsync(Mentor mentor, Subject subject);

        Task<Subject> GetSubjectAsync(SubjectModel subjectModel);
        Task<Subject> UpdateSubjectAsync(Subject subject);

        Task<StudyGroup> GetGroupAsync(StudyGroupModel groupModel);
        Task<StudyGroup> UpdateGroupAsync(StudyGroup group);

        Task<StudentsAssignmentProgressTable> GetStudentAssignmentProgressTableAsync(StudyGroupSubject studyGroupSubject);
        Task SetStudentAssignmentProgressesAsync(IReadOnlyCollection<StudentAssignmentProgress> progresses);
    }
}