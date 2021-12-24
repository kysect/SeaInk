using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;
using SeaInk.Core.Services;
using SeaInk.Infrastructure.DataAccess.Database;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Queries;

public class GetStudyStudentGroupSheet
{
    public record Query(Mentor Mentor, Guid StudyStudentGroupId) : IRequest<SheetLink>;

    public class Handler : IRequestHandler<Query, SheetLink>
    {
        private readonly DatabaseContext _context;
        private readonly ITableService _tableService;

        public Handler(ITableService tableService, DatabaseContext context)
        {
            _tableService = tableService;
            _context = context;
        }

        public async Task<SheetLink> Handle(Query request, CancellationToken cancellationToken)
        {
            StudyStudentGroup? ssg = await _context.StudyStudentGroups
                .FindAsync(new object[] { request.StudyStudentGroupId }, cancellationToken)
                .ConfigureAwait(false);
            ssg = ssg.ThrowIfNull();

            return await _tableService.GetSheetLinkAsync(ssg, cancellationToken);
        }
    }
}