using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SeaInk.Core.Entities;
using SeaInk.Core.Services;
using SeaInk.Core.StudyTable;
using SeaInk.Infrastructure.DataAccess.Database;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Commands;

public static class ApplyStudyStudentGroupTableDifference
{
    public record Command(Mentor Mentor, Guid StudyStudentGroupId, StudentAssignmentProgressTableDifference Difference) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly DatabaseContext _context;
        private readonly ITableDifferenceService _tableDifferenceService;

        public Handler(DatabaseContext context, ITableDifferenceService tableDifferenceService)
        {
            _context = context;
            _tableDifferenceService = tableDifferenceService;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            StudyStudentGroup? studyStudentGroup = await _context.StudyStudentGroups
                .FindAsync(new object[] { request.StudyStudentGroupId }, cancellationToken)
                .ConfigureAwait(false);
            studyStudentGroup = studyStudentGroup.ThrowIfNull();

            await _tableDifferenceService.ApplyDifference(studyStudentGroup, request.Difference, cancellationToken);
            return default;
        }
    }
}