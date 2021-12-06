using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Kysect.Centum.Fields;
using Kysect.Centum.Fields.Extractors;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Infrastructure.Exceptions;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.TableLayout
{
    public class GoogleTableDataProvider : ITableDataProvider
    {
        private readonly IReadOnlyList<IReadOnlyList<string>> _data;

        private GoogleTableDataProvider(IReadOnlyList<IReadOnlyList<string>> data)
        {
            _data = data;
            Frame = new Frame(data.Max(d => d.Count), data.Count);
        }

        public Frame Frame { get; }
        public string this[ISheetIndex index] => _data[index.Column.Value][index.Row.Value];

        public static async Task<ITableDataProvider> Create(SheetsService service, string spreadsheetId, int sheetId)
        {
            service.ThrowIfNull();
            spreadsheetId.ThrowIfNull();

            Spreadsheet spreadsheet = await GetSpreadsheet(service, spreadsheetId);
            Sheet sheet = spreadsheet.Sheets
                .SingleOrDefault(s => s.Properties.SheetId.Equals(sheetId))
                .ThrowIfNull(new SheetNotFoundException(spreadsheetId, sheetId));

            GridProperties gridProperties = sheet.Properties.GridProperties;

            var range = new SheetIndexRange(
                new SheetIndex(1, 1),
                new SheetIndex(gridProperties.ColumnCount.ThrowIfNull() + 1, gridProperties.RowCount.ThrowIfNull() + 1));

            ValueRange valueRange = await service.Spreadsheets.Values
                .Get(spreadsheetId, $"{sheet.Properties.Title}!{range}")
                .ExecuteAsync();

            var data = valueRange.Values
                .Select(d => (IReadOnlyList<string>)d.Select(dd => dd.ToString()).ToList())
                .ToList();

            return new GoogleTableDataProvider(data);
        }

        private static Task<Spreadsheet> GetSpreadsheet(SheetsService service, string spreadsheetId)
        {
            FieldPathExtractor<Spreadsheet> sheetsExtractor = Fields<Spreadsheet>.FromSequence(
                s => s.Sheets,
                s => s.Properties.GridProperties.ColumnCount,
                s => s.Properties.GridProperties.RowCount,
                s => s.Properties.Title);

            SpreadsheetsResource.GetRequest request = service.Spreadsheets.Get(spreadsheetId);
            request.Fields = sheetsExtractor.ToString();

            return request.ExecuteAsync();
        }
    }
}