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

        Task<IReadOnlyCollection<SubjectUniversityModel>> GetMentorSubjectUniversityModelsAsync(Mentor mentor, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<StudentGroupUniversityModel>> GetMentorSubjectGroupUniversityModelsAsync(Mentor mentor, Subject subject, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<AssignmentUniversityModel>> GetSubjectAssignmentUniversityModelsAsync(Subject subject, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<StudentUniversityModel>> GetGroupStudentUniversityModelsAsync(StudentGroup group, CancellationToken cancellationToken);

        Task<StudentsAssignmentProgressTable> GetStudentAssignmentProgressTableAsync(StudyStudentGroup studyStudentGroup, CancellationToken cancellationToken);
        Task SetStudentAssignmentProgressesAsync(IReadOnlyCollection<StudentAssignmentProgress> progresses, CancellationToken cancellationToken);
    }
}