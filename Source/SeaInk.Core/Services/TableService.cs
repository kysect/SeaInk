using System.Threading;
using System.Threading.Tasks;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;
using SeaInk.Core.Services.Exceptions;
using SeaInk.Core.TableLayout;
using SeaInk.Core.TableLayout.Commands;
using SeaInk.Core.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Services
{
    public class TableService : ITableService
    {
        private readonly ISheetsService _sheetsService;

        public TableService(ISheetsService sheetsService)
        {
            _sheetsService = sheetsService.ThrowIfNull();
        }

        public async Task<CreateSpreadsheetResponse> CreateSpreadsheetAsync(
            StudyStudentGroup studyStudentGroup, TableLayoutComponent layoutComponent, CancellationToken cancellationToken)
        {
            studyStudentGroup.ThrowIfNull();
            layoutComponent.ThrowIfNull();

            SubjectDivision subjectDivision = studyStudentGroup.Division.ThrowIfNull();

            if (!string.IsNullOrEmpty(subjectDivision.SpreadsheetId))
                throw new SpreadsheetCreatedException(subjectDivision);

            CreateSpreadsheetResponse response = await _sheetsService.CreateSpreadsheetAsync(subjectDivision.Subject.Name, cancellationToken);
            ITableEditor editor = await _sheetsService.GetEditorFor(response.SheetInfo, cancellationToken);

            layoutComponent.ExecuteCommand(new DrawAllCommand(), new SheetIndex(1, 1), editor);
            await editor.ExecuteAsync().ConfigureAwait(false);

            return response;
        }

        public async Task<CreateSheetResponse> CreateSheetAsync(
            StudyStudentGroup studyStudentGroup, TableLayoutComponent layoutComponent, CancellationToken cancellationToken)
        {
            studyStudentGroup.ThrowIfNull();
            layoutComponent.ThrowIfNull();

            SubjectDivision subjectDivision = studyStudentGroup.Division.ThrowIfNull();

            if (string.IsNullOrEmpty(subjectDivision.SpreadsheetId))
                throw new SpreadsheetNotCreatedException(subjectDivision);

            if (studyStudentGroup.SheetId is not null)
                throw new SheetCreatedException(studyStudentGroup);

            CreateSheetResponse response = await _sheetsService
                .CreateSheetAsync(subjectDivision.SpreadsheetId, studyStudentGroup.StudentGroup.Name, cancellationToken);

            ITableEditor editor = await _sheetsService.GetEditorFor(new SheetInfo(subjectDivision.SpreadsheetId, response.SheetId), cancellationToken);

            layoutComponent.ExecuteCommand(new DrawAllCommand(), new SheetIndex(1, 1), editor);
            await editor.ExecuteAsync().ConfigureAwait(false);

            return response;
        }

        public Task<SheetLink> GetSheetLinkAsync(StudyStudentGroup studyStudentGroup, CancellationToken cancellationToken)
        {
            SubjectDivision division = studyStudentGroup.Division.ThrowIfNull();

            if (string.IsNullOrEmpty(division.SpreadsheetId))
                throw new SpreadsheetNotCreatedException(division);

            if (studyStudentGroup.SheetId is null)
                throw new SheetNotCreatedException(studyStudentGroup);

            return _sheetsService.GetSheetLinkAsync(division.SpreadsheetId, studyStudentGroup.SheetId.Value, cancellationToken);
        }

        public async Task WriteDataToSheetAsync(
            SubjectDivision subjectDivision,
            StudyStudentGroup studyStudentGroup,
            TableLayoutComponent layoutComponent,
            TableModel tableModel,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(subjectDivision.SpreadsheetId))
                throw new SpreadsheetNotCreatedException(subjectDivision);

            int sheetId = studyStudentGroup.SheetId
                .ThrowIfNull(new SheetNotCreatedException(studyStudentGroup));

            ITableEditor editor = await _sheetsService.GetEditorFor(new SheetInfo(subjectDivision.SpreadsheetId, sheetId), cancellationToken);

            layoutComponent.SetTable(tableModel, editor);
            await editor.ExecuteAsync().ConfigureAwait(false);
        }

        public async Task<TableModel> GetDataFromSheetAsync(
            SubjectDivision subjectDivision,
            StudyStudentGroup studyStudentGroup,
            TableLayoutComponent layoutComponent,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(subjectDivision.SpreadsheetId))
                throw new SpreadsheetNotCreatedException(subjectDivision);

            int sheetId = studyStudentGroup.SheetId
                .ThrowIfNull(new SheetNotCreatedException(studyStudentGroup));

            ITableDataProvider provider = await _sheetsService
                .GetDataProviderAsync(new SheetInfo(subjectDivision.SpreadsheetId, sheetId), cancellationToken)
                .ConfigureAwait(false);

            return layoutComponent.GetTable(provider);
        }
    }
}