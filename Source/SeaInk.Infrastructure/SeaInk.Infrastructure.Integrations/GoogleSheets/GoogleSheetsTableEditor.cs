using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Kysect.Centum.Extensions.Sheets.Indices;
using Kysect.Centum.Sheets.Indices;
using Kysect.Centum.Sheets.Models;
using SeaInk.Core.TableLayout;
using SeaInk.Core.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.Integrations.GoogleSheets
{
    public class GoogleSheetsTableEditor : ITableEditor
    {
        private readonly object _lock = new object();

        private readonly SheetsService _service;
        private readonly string _spreadsheetId;
        private readonly int _sheetId;

        private readonly List<Request> _requests = new List<Request>();
        private readonly List<ValueRange> _ranges = new List<ValueRange>();

        public GoogleSheetsTableEditor(SheetsService service, string spreadsheetId, int sheetId)
        {
            _service = service;
            _spreadsheetId = spreadsheetId.ThrowIfNull();
            _sheetId = sheetId;
        }

        public void EnqueueWrite(ISheetIndex index, IReadOnlyCollection<IReadOnlyCollection<string>> data)
        {
            int width = data.Max(d => d.Count);
            ISheetIndexRange indexRange = new SheetIndexRange(index, new SheetIndex(width, data.Count));

            var range = new ValueRange
            {
                MajorDimension = Dimension.Rows,
                Values = data.Select(d => (IList<object>)d.Select(v => (object)v).ToList()).ToList(),
                Range = indexRange.ToString(),
            };

            _ranges.Add(range);
        }

        public void EnqueueMerge(ISheetIndex index, Frame frame)
        {
            var request = new MergeCellsRequest
            {
                MergeType = MergeType.All,
                Range = new SheetIndexRange(index, index.Add(new SheetIndex(frame.Width, frame.Height))).ToGoogleGridRange(_sheetId),
            };

            _requests.Add(new Request { MergeCells = request });
        }

        public void EnqueueInsertColumn(ColumnIndex column)
        {
            var request = new InsertDimensionRequest
            {
                Range = NewDimensionRange(Dimension.Columns, _sheetId, column.Value, column.Value + 1),
            };

            _requests.Add(new Request { InsertDimension = request });
        }

        public void EnqueueInsertRow(RowIndex row)
        {
            var request = new InsertDimensionRequest
            {
                Range = NewDimensionRange(Dimension.Columns, _sheetId, row.Value, row.Value + 1),
            };

            _requests.Add(new Request { InsertDimension = request });
        }

        public void EnqueueDeleteColumn(ColumnIndex column)
        {
            var request = new DeleteDimensionRequest
            {
                Range = NewDimensionRange(Dimension.Columns, _sheetId, column.Value, column.Value + 1),
            };

            _requests.Add(new Request { DeleteDimension = request });
        }

        public void EnqueueDeleteRow(RowIndex row)
        {
            var request = new DeleteDimensionRequest
            {
                Range = NewDimensionRange(Dimension.Columns, _sheetId, row.Value, row.Value + 1),
            };

            _requests.Add(new Request { DeleteDimension = request });
        }

        public async Task ExecuteAsync()
        {
            IList<Request> requests;
            IList<ValueRange> ranges;

            lock (_lock)
            {
                requests = _requests.ToList();
                ranges = _ranges.ToList();

                _requests.Clear();
                _ranges.Clear();
            }

            var spreadsheetBody = new BatchUpdateSpreadsheetRequest
            {
                Requests = requests,
            };

            var valuesBody = new BatchUpdateValuesRequest
            {
                ValueInputOption = ValueInputOption.UserEntered,
                Data = ranges,
            };

            SpreadsheetsResource.BatchUpdateRequest spreadsheetRequest = _service.Spreadsheets.BatchUpdate(spreadsheetBody, _spreadsheetId);
            SpreadsheetsResource.ValuesResource.BatchUpdateRequest valuesRequest = _service.Spreadsheets.Values.BatchUpdate(valuesBody, _spreadsheetId);

            await Task.WhenAll(spreadsheetRequest.ExecuteAsync(), valuesRequest.ExecuteAsync());
        }

        private static DimensionRange NewDimensionRange(Dimension dimension, int sheetId, int start, int end)
        {
            return new DimensionRange
            {
                Dimension = dimension,
                SheetId = sheetId,
                StartIndex = start,
                EndIndex = end,
            };
        }
    }
}