using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SeaInk.Core.Entities;
using SeaInk.Core.UniversityServiceModels;

namespace SeaInk.Application.Services;

public interface IDatabaseSynchronizationService
{
    Task<IReadOnlyCollection<Subject>> SynchronizeSubjectsAsync(IReadOnlyCollection<SubjectUniversityModel> models, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Subject>> SynchronizeSubjectsAsync(IReadOnlyCollection<Subject> subjects, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<StudentGroup>> SynchronizeStudentGroupsAsync(
        IReadOnlyCollection<StudentGroupUniversityModel> models, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<StudentGroup>> SynchronizeStudentGroupsAsync(
        IReadOnlyCollection<StudentGroup> groups, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<StudyStudentGroup>> SynchronizeStudyStudentGroupsAsync(
        Subject subject, IReadOnlyCollection<StudentGroup> groups, CancellationToken cancellationToken);
}