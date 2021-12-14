using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SeaInk.Application.Services;
using SeaInk.Core.Entities;
using SeaInk.Core.Services;
using SeaInk.Core.UniversityServiceModels;
using SeaInk.Infrastructure.DataAccess.Database;

namespace SeaInk.Application.Commands;

public static class UpdateMentor
{
    public record Command(Mentor Mentor) : IRequest<Mentor>;

    public class Handler : IRequestHandler<Command, Mentor>
    {
        private readonly DatabaseContext _context;
        private readonly IUniversityService _universityService;
        private readonly IDatabaseSynchronizationService _databaseSynchronizationService;

        public Handler(DatabaseContext context, IUniversityService universityService, IDatabaseSynchronizationService databaseSynchronizationService)
        {
            _context = context;
            _universityService = universityService;
            _databaseSynchronizationService = databaseSynchronizationService;
        }

        public async Task<Mentor> Handle(Command request, CancellationToken cancellationToken)
        {
            Mentor mentor = request.Mentor;

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

                IReadOnlyCollection<StudentGroup> mentorSubjectGroups = await _databaseSynchronizationService
                    .SynchronizeStudentGroupsAsync(groupModels, cancellationToken)
                    .ConfigureAwait(false);

                IReadOnlyCollection<StudyStudentGroup> mentorSubjectStudyStudentGroups = await _databaseSynchronizationService
                    .SynchronizeStudyStudentGroupsAsync(subject, mentorSubjectGroups, cancellationToken)
                    .ConfigureAwait(false);

                SubjectDivision subjectDivision = await _context.SubjectDivisions
                    .SingleAsync(sd => sd.Subject.Equals(subject), cancellationToken)
                    .ConfigureAwait(false);

                var assignedMentorSubjectStudyStudentGroups = subjectDivision.StudyStudentGroups
                    .Where(ssg => ssg.Mentors.Contains(mentor))
                    .ToList();

                var studyStudentGroupsToUnAssign = assignedMentorSubjectStudyStudentGroups
                    .Except(mentorSubjectStudyStudentGroups)
                    .ToList();

                var studyStudentGroupsToAssign = mentorSubjectStudyStudentGroups
                    .Except(assignedMentorSubjectStudyStudentGroups)
                    .ToList();

                studyStudentGroupsToUnAssign.ForEach(ssg => ssg.RemoveMentors(mentor));
                _context.StudyStudentGroups.UpdateRange(studyStudentGroupsToUnAssign);

                studyStudentGroupsToAssign.ForEach(ssg => ssg.AddMentors(mentor));
                _context.StudyStudentGroups.UpdateRange(studyStudentGroupsToAssign);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return mentor;
        }
    }
}