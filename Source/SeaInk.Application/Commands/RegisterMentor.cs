using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SeaInk.Application.Extensions;
using SeaInk.Application.Services;
using SeaInk.Core.Entities;
using SeaInk.Core.Services;
using SeaInk.Core.UniversityServiceModels;
using SeaInk.Infrastructure.DataAccess.Database;
using SeaInk.Infrastructure.DataAccess.Extensions;

namespace SeaInk.Application.Commands;

public static class RegisterMentor
{
    public record Command(int MentorUniversityId) : IRequest<Mentor>;

    public class Handler : IRequestHandler<Command, Mentor>
    {
        private readonly IUniversityService _universityService;
        private readonly IDatabaseSynchronizationService _databaseSynchronizationService;
        private readonly DatabaseContext _context;

        public Handler(IUniversityService universityService, DatabaseContext context, IDatabaseSynchronizationService databaseSynchronizationService)
        {
            _databaseSynchronizationService = databaseSynchronizationService;
            _universityService = universityService;
            _context = context;
        }

        public async Task<Mentor> Handle(Command request, CancellationToken cancellationToken)
        {
            Mentor mentor = await _universityService
                .GetMentorAsync(request.MentorUniversityId, cancellationToken)
                .ConfigureAwait(false);

            IReadOnlyCollection<SubjectUniversityModel> subjectModels = await _universityService
                .GetMentorSubjectsAsync(mentor, cancellationToken)
                .ConfigureAwait(false);

            IReadOnlyCollection<Subject> subjects = await _databaseSynchronizationService
                .SynchronizeSubjectsAsync(subjectModels, cancellationToken)
                .ConfigureAwait(false);

            foreach (Subject subject in subjects)
            {
                IReadOnlyCollection<StudentGroupUniversityModel> groupModels = await _universityService
                    .GetMentorSubjectGroupsAsync(mentor, subject, cancellationToken)
                    .ConfigureAwait(false);

                IReadOnlyCollection<StudentGroup> groups = await _databaseSynchronizationService
                    .SynchronizeStudentGroupsAsync(groupModels, cancellationToken)
                    .ConfigureAwait(false);

                IReadOnlyCollection<StudyStudentGroup> studyGroups = await _databaseSynchronizationService
                    .SynchronizeStudyStudentGroupsAsync(subject, groups, cancellationToken)
                    .ConfigureAwait(false);

                studyGroups.ForEach(ssg => ssg.AddMentors(mentor));
                _context.StudyStudentGroups.UpdateRange(studyGroups);
                await _context.SaveChangesAsync(cancellationToken);
            }

            _context.Mentors.AddOrUpdate(mentor);
            await _context.SaveChangesAsync(cancellationToken);
            return mentor;
        }
    }
}