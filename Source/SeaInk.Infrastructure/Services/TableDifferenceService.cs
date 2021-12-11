using System.Linq;
using System.Threading.Tasks;
using SeaInk.Application.Exceptions;
using SeaInk.Application.Extensions;
using SeaInk.Application.Models;
using SeaInk.Application.Services;
using SeaInk.Application.TableLayout;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.Services
{
    public class TableDifferenceService : ITableDifferenceService
    {
        private readonly IUniversityService _universityService;
        private readonly ISheetsService _sheetsService;
        private readonly ILayoutService _layoutService;

        public TableDifferenceService(IUniversityService universityService, ISheetsService sheetsService, ILayoutService layoutService)
        {
            _universityService = universityService.ThrowIfNull();
            _sheetsService = sheetsService.ThrowIfNull();
            _layoutService = layoutService.ThrowIfNull();
        }

        public async Task<StudentAssignmentProgressTableDifference> CalculateDifference(StudyGroupSubject studyGroupSubject)
        {
            TableLayoutComponent? layout = await _layoutService.GetLayoutAsync(studyGroupSubject);

            if (layout is null)
                throw new MissingLayoutException(studyGroupSubject);

            if (string.IsNullOrEmpty(studyGroupSubject.Division?.SpreadsheetId))
                throw new SheetNotCreatedException(studyGroupSubject);

            if (studyGroupSubject.SheetId is null)
                throw new SheetNotCreatedException(studyGroupSubject);

            ITableDataProvider dataProvider = await _sheetsService.GetDataProviderAsync(
                new SheetInfo(studyGroupSubject.Division.SpreadsheetId, studyGroupSubject.SheetId.Value));

            var sheetsTable = layout.GetTable(dataProvider).ToStudentsAssignmentProgressTable(studyGroupSubject);
            var universityTable = await _universityService.GetStudentAssignmentProgressTableAsync(studyGroupSubject);

            return new StudentAssignmentProgressTableDifference(sheetsTable, universityTable);
        }

        public Task ApplyDifference(StudyGroupSubject studyGroupSubject, StudentAssignmentProgressTableDifference difference)
        {
            var progresses = difference.AssignmentProgressDifferences
                .Select(d => new StudentAssignmentProgress(d.Student, d.Assignment, d.NewProgress ?? new AssignmentProgress(0)))
                .ToList();

            // TODO:
            // Added & Removed students handling.
            // Added & Removed assignments handling.
            return _universityService.SetStudentAssignmentProgressesAsync(progresses);
        }
    }
}