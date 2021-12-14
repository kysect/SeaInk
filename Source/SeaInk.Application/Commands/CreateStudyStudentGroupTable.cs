using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SeaInk.Application.Commands.Exceptions;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;
using SeaInk.Core.Services;
using SeaInk.Core.TableLayout;
using SeaInk.Infrastructure.DataAccess.Database;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Commands;

public static class CreateStudyStudentGroupTable
{
    public record Command(Mentor Mentor, Guid StudyStudentGroupId, TableLayoutComponent LayoutComponent) : IRequest<CreateSheetResponse>;

    public class Handler : IRequestHandler<Command, CreateSheetResponse>
    {
        private readonly DatabaseContext _context;
        private readonly ITableService _tableService;

        public Handler(DatabaseContext context, ITableService tableService)
        {
            _context = context.ThrowIfNull();
            _tableService = tableService.ThrowIfNull();
        }

        public async Task<CreateSheetResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            StudyStudentGroup? ssg = await _context.StudyStudentGroups
                .FindAsync(new object[] { request.StudyStudentGroupId }, cancellationToken)
                .ConfigureAwait(false);

            ssg = ssg.ThrowIfNull();

            if (ssg.Division is null)
                throw new DivisionUnassignedException(ssg);

            if (string.IsNullOrEmpty(ssg.Division.SpreadsheetId))
            {
                CreateSpreadsheetResponse response = await _tableService
                    .CreateSpreadsheetAsync(ssg, request.LayoutComponent, cancellationToken)
                    .ConfigureAwait(false);

                ssg.Division.SpreadsheetId = response.SheetInfo.SpreadsheetId;
                ssg.SheetId = response.SheetInfo.SheetId;

                _context.StudyStudentGroups.UpdateRange(ssg);
                _context.SubjectDivisions.UpdateRange(ssg.Division);
                await _context.SaveChangesAsync(cancellationToken);

                return new CreateSheetResponse(response.SheetInfo.SheetId, response.SheetLink);
            }
            else
            {
                CreateSheetResponse response = await _tableService
                    .CreateSheetAsync(ssg, request.LayoutComponent, cancellationToken)
                    .ConfigureAwait(false);

                ssg.SheetId = response.SheetId;

                _context.StudyStudentGroups.Update(ssg);
                await _context.SaveChangesAsync(cancellationToken);

                return response;
            }
        }
    }
}