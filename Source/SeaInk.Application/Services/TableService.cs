using System;
using System.Threading.Tasks;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.Exceptions;
using SeaInk.Application.Models;
using SeaInk.Application.TableLayout;
using SeaInk.Application.TableLayout.Commands;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Core.Entities;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Services
{
    public class TableService : ITableService
    {
        private readonly ILayoutService _layoutService;
        private readonly ISheetsService _sheetsService;

        public TableService(ILayoutService layoutService, ISheetsService sheetsService)
        {
            _layoutService = layoutService.ThrowIfNull();
            _sheetsService = sheetsService.ThrowIfNull();
        }

        public async Task<CreateSpreadsheetResponse> CreateSpreadsheetAsync(StudyGroupSubject studyGroupSubject, TableLayoutComponent layoutComponent)
        {
            studyGroupSubject.ThrowIfNull();
            layoutComponent.ThrowIfNull();

            Division division = studyGroupSubject.Division.ThrowIfNull();

            if (!string.IsNullOrEmpty(division.SpreadsheetId))
                throw new SpreadsheetCreatedException(division);

            CreateSpreadsheetResponse response = await _sheetsService.CreateSpreadsheetAsync(division.Title);
            ITableEditor editor = _sheetsService.GetEditorFor(response.SheetInfo);
            division.SpreadsheetId = response.SheetInfo.SpreadsheetId;
            studyGroupSubject.SheetId = response.SheetInfo.SheetId;

            layoutComponent.ExecuteCommand(new DrawAllCommand(), new SheetIndex(1, 1), editor);
            await _layoutService.SaveLayoutAsync(studyGroupSubject, layoutComponent).ConfigureAwait(false);
            await editor.ExecuteAsync().ConfigureAwait(false);

            return response;
        }

        public async Task<CreateSheetResponse> CreateSheetAsync(StudyGroupSubject studyGroupSubject, TableLayoutComponent layoutComponent)
        {
            studyGroupSubject.ThrowIfNull();
            layoutComponent.ThrowIfNull();

            Division division = studyGroupSubject.Division.ThrowIfNull();

            if (string.IsNullOrEmpty(division.SpreadsheetId))
                throw new SpreadsheetNotCreatedException(division);

            if (studyGroupSubject.SheetId is not null)
                throw new SheetCreatedException(studyGroupSubject);

            CreateSheetResponse response = await _sheetsService.CreateSheetAsync(division.SpreadsheetId, studyGroupSubject.StudyGroup.Name);
            ITableEditor editor = _sheetsService.GetEditorFor(new SheetInfo(division.SpreadsheetId, response.SheetId));
            studyGroupSubject.SheetId = response.SheetId;

            layoutComponent.ExecuteCommand(new DrawAllCommand(), new SheetIndex(1, 1), editor);
            await _layoutService.SaveLayoutAsync(studyGroupSubject, layoutComponent).ConfigureAwait(false);
            await editor.ExecuteAsync().ConfigureAwait(false);

            return response;
        }

        public async Task WriteDataToSheetAsync(Division division, StudyGroupSubject studyGroupSubject, TableModel tableModel)
        {
            if (string.IsNullOrEmpty(division.SpreadsheetId))
                throw new SpreadsheetNotCreatedException(division);

            int sheetId = studyGroupSubject.SheetId
                .ThrowIfNull(new SheetNotCreatedException(studyGroupSubject));

            TableLayoutComponent? layoutComponent = await _layoutService
                .GetLayoutAsync(studyGroupSubject)
                .ConfigureAwait(false);

            if (layoutComponent is null)
                throw new MissingLayoutException(studyGroupSubject);

            ITableEditor editor = _sheetsService.GetEditorFor(new SheetInfo(division.SpreadsheetId, sheetId));

            layoutComponent.SetTable(tableModel, editor);
            await editor.ExecuteAsync().ConfigureAwait(false);
        }

        public async Task<TableModel> GetDataFromSheetAsync(Division division, StudyGroupSubject studyGroupSubject)
        {
            if (string.IsNullOrEmpty(division.SpreadsheetId))
                throw new SpreadsheetNotCreatedException(division);

            int sheetId = studyGroupSubject.SheetId
                .ThrowIfNull(new SheetNotCreatedException(studyGroupSubject));

            TableLayoutComponent? layoutComponent = await _layoutService
                .GetLayoutAsync(studyGroupSubject)
                .ConfigureAwait(false);

            if (layoutComponent is null)
                throw new MissingLayoutException(studyGroupSubject);

            ITableDataProvider provider = await _sheetsService
                .GetDataProviderAsync(new SheetInfo(division.SpreadsheetId, sheetId))
                .ConfigureAwait(false);

            return layoutComponent.GetTable(provider);
        }
    }
}