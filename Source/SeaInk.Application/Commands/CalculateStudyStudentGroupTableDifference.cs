using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SeaInk.Application.Services;
using SeaInk.Core.Entities;
using SeaInk.Core.Services;
using SeaInk.Core.StudyTable;
using SeaInk.Infrastructure.DataAccess.Database;
using SeaInk.Infrastructure.DataAccess.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Commands;

public static class CalculateStudyStudentGroupTableDifference
{
    public record Command(Mentor Mentor, Guid StudyStudentGroupId) : IRequest<StudentAssignmentProgressTableDifference>;

    public class Handler : IRequestHandler<Command, StudentAssignmentProgressTableDifference>
    {
        private readonly IDatabaseSynchronizationService _databaseSynchronizationService;
        private readonly ITableDifferenceService _tableDifferenceService;
        private readonly DatabaseContext _context;

        public Handler(IDatabaseSynchronizationService databaseSynchronizationService, ITableDifferenceService tableDifferenceService, DatabaseContext context)
        {
            _databaseSynchronizationService = databaseSynchronizationService;
            _tableDifferenceService = tableDifferenceService;
            _context = context;
        }

        public async Task<StudentAssignmentProgressTableDifference> Handle(Command request, CancellationToken cancellationToken)
        {
            StudyStudentGroup? studyStudentGroup = await _context.StudyStudentGroups
                .FindAsync(new object[] { request.StudyStudentGroupId }, cancellationToken)
                .ConfigureAwait(false);
            studyStudentGroup = studyStudentGroup.ThrowIfNull();

            await _databaseSynchronizationService
                .SynchronizeStudentGroupsAsync(new[] { studyStudentGroup.StudentGroup }, cancellationToken)
                .ConfigureAwait(false);

            SubjectDivision division = studyStudentGroup.Division.ThrowIfNull();
            StudyGroupSubjectLayout layout = await _context.StudyGroupSubjectLayouts
                .SingleAsync(l => l.StudyStudentGroup.Equals(studyStudentGroup), cancellationToken)
                .ConfigureAwait(false);

            StudentAssignmentProgressTableDifference difference = await _tableDifferenceService
                .CalculateDifference(studyStudentGroup, division.Subject, layout.Layout, cancellationToken)
                .ConfigureAwait(false);

            return difference;
        }
    }
}