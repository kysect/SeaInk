using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Kysect.Centum.Fields;
using Kysect.Centum.Fields.Extractors;
using SeaInk.Core.Models;
using SeaInk.Core.Services;
using SeaInk.Core.TableLayout;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.Integrations.GoogleSheets
{
    public class GoogleSheetsService : ISheetsService
    {
        private readonly SheetsService _service;

        public GoogleSheetsService(SheetsService service)
        {
            _service = service;
        }

        public async Task<CreateSpreadsheetResponse> CreateSpreadsheetAsync(string title, CancellationToken cancellationToken)
        {
            var body = new Spreadsheet
            {
                Properties = new SpreadsheetProperties { Title = title, },
            };

            FieldPathExtractor<Spreadsheet> urlExtractor = Fields<Spreadsheet>.From(s => s.SpreadsheetUrl);
            FieldPathExtractor<Spreadsheet> sheetsExtractor = Fields<Spreadsheet>
                .FromSequence(s => s.Sheets, s => s.Properties.SheetId);

            SpreadsheetsResource.CreateRequest request = _service.Spreadsheets.Create(body);
            request.Fields = new[] { urlExtractor, sheetsExtractor }.StringRepresentation();

            Spreadsheet response = await request.ExecuteAsync(cancellationToken);

            Sheet? sheet = response.Sheets.Single();

            return new CreateSpreadsheetResponse(
                new SheetInfo(response.SpreadsheetId, sheet.Properties.SheetId.ThrowIfNull()),
                new SheetLink(response.SpreadsheetUrl.ThrowIfNull()));
        }

        public async Task<CreateSheetResponse> CreateSheetAsync(string spreadsheetId, string title, CancellationToken cancellationToken)
        {
            var updateRequest = new Request
            {
                AddSheet = new AddSheetRequest { Properties = new SheetProperties { Title = title } },
            };

            var body = new BatchUpdateSpreadsheetRequest
            {
                Requests = new[] { updateRequest },
                IncludeSpreadsheetInResponse = true,
            };

            FieldPathExtractor<Spreadsheet> sheetExtractor = Fields<Spreadsheet>.FromSequence(
                s => s.Sheets,
                s => s.Properties.SheetId);
            FieldPathExtractor<Spreadsheet> urlExtractor = Fields<Spreadsheet>.From(s => s.SpreadsheetUrl);

            SpreadsheetsResource.BatchUpdateRequest request = _service.Spreadsheets.BatchUpdate(body, spreadsheetId);
            request.Fields = new[] { sheetExtractor, urlExtractor }.StringRepresentation();

            BatchUpdateSpreadsheetResponse? response = await request.ExecuteAsync(cancellationToken);
            Spreadsheet spreadsheet = response.UpdatedSpreadsheet;
            Sheet sheet = spreadsheet.Sheets.Single(s => s.Properties.Title.Equals(title));

            return new CreateSheetResponse(
                sheet.Properties.SheetId.ThrowIfNull(),
                new SheetLink($"{spreadsheet.SpreadsheetUrl}#gid={sheet.Properties.SheetId}"));
        }

        public Task<ITableEditor> GetEditorFor(SheetInfo info, CancellationToken cancellationToken)
            => Task.FromResult((ITableEditor)new GoogleSheetsTableEditor(_service, info.SpreadsheetId, info.SheetId));

        public Task<ITableDataProvider> GetDataProviderAsync(SheetInfo info, CancellationToken cancellationToken)
            => GoogleTableDataProvider.CreateAsync(_service, info.SpreadsheetId, info.SheetId, cancellationToken);

        public Task<SheetLink> GetSheetLinkAsync(string spreadsheetId, int sheetId, CancellationToken cancellationToken)
            => Task.FromResult(new SheetLink($"https://docs.google.com/spreadsheets/d/{spreadsheetId}/edit#gid={sheetId}"));
    }
}