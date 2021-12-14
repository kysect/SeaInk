using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;
using SeaInk.Core.StudyTable;
using SeaInk.Core.UniversityServiceModels;

namespace SeaInk.Core.Services
{
    public interface IUniversityService
    {
        Task<Mentor> GetMentorAsync(int universityId, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<SubjectUniversityModel>> GetMentorSubjectsAsync(Mentor mentor, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<StudentGroupUniversityModel>> GetMentorSubjectGroupsAsync(Mentor mentor, Subject subject, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<AssignmentUniversityModel>> GetSubjectAssignmentsAsync(Subject subject, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<StudentUniversityModel>> GetGroupStudentsAsync(StudentGroup group, CancellationToken cancellationToken);

        Task<StudentsAssignmentProgressTable> GetStudentAssignmentProgressTableAsync(StudyStudentGroup studyStudentGroup, CancellationToken cancellationToken);
        Task SetStudentAssignmentProgressesAsync(IReadOnlyCollection<StudentAssignmentProgress> progresses, CancellationToken cancellationToken);
    }
}