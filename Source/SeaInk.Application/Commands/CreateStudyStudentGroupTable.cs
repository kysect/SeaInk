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

            CreateSheetResponse response = await _tableService
                .CreateSheetAsync(ssg, request.LayoutComponent, cancellationToken)
                .ConfigureAwait(false);

            _context.StudyStudentGroups.Update(ssg);
            _context.SubjectDivisions.Update(ssg.Division);
            await _context.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}