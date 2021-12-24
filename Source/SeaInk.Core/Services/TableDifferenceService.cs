using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SeaInk.Core.Entities;
using SeaInk.Core.Extensions;
using SeaInk.Core.Models;
using SeaInk.Core.Services.Exceptions;
using SeaInk.Core.StudyTable;
using SeaInk.Core.TableLayout;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Services
{
    public class TableDifferenceService : ITableDifferenceService
    {
        private readonly IUniversityService _universityService;
        private readonly ISheetsService _sheetsService;

        public TableDifferenceService(IUniversityService universityService, ISheetsService sheetsService)
        {
            _universityService = universityService.ThrowIfNull();
            _sheetsService = sheetsService.ThrowIfNull();
        }

        public async Task<StudentAssignmentProgressTableDifference> CalculateDifference(
            StudyStudentGroup studyStudentGroup,
            Subject subject,
            TableLayoutComponent layoutComponent,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(studyStudentGroup.Division?.SpreadsheetId))
                throw new SheetNotCreatedException(studyStudentGroup);

            if (studyStudentGroup.SheetId is null)
                throw new SheetNotCreatedException(studyStudentGroup);

            ISheetDataProvider dataProvider = await _sheetsService.GetDataProviderAsync(
                new SheetInfo(studyStudentGroup.Division.SpreadsheetId, studyStudentGroup.SheetId.Value), cancellationToken);

            var sheetsTable = layoutComponent.GetTable(dataProvider).ToStudentsAssignmentProgressTable(studyStudentGroup, subject);
            var universityTable = await _universityService.GetStudentAssignmentProgressTableAsync(studyStudentGroup, cancellationToken);

            return new StudentAssignmentProgressTableDifference(sheetsTable, universityTable);
        }

        public Task ApplyDifference(
            StudyStudentGroup studyStudentGroup, StudentAssignmentProgressTableDifference difference, CancellationToken cancellationToken)
        {
            var progresses = difference.AssignmentProgressDifferences
                .Select(d => new StudentAssignmentProgress(d.Student, d.Assignment, d.NewProgress ?? new AssignmentProgress(0)))
                .ToList();

            // TODO:
            // Added & Removed students handling.
            // Added & Removed assignments handling.
            return _universityService.SetStudentAssignmentProgressesAsync(progresses, cancellationToken);
        }
    }
}