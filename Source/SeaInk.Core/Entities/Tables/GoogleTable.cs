using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using SeaInk.Core.Models.Tables;
using SeaInk.Core.Models.Tables.Enums;
using SeaInk.Core.Models.Tables.Exceptions;
using SeaInk.Core.Utils;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum;

namespace SeaInk.Core.Entities.Tables
{
    /// <inheritdoc cref="ITable"/>
    public class GoogleTable : ITable
    {
        private static readonly string[] Scopes = {SheetsService.Scope.Spreadsheets};
        private const string ApplicationName = "Sea Ink";

        private string SpreadsheetId { get; set; }
        private SheetsService Service { get; set; }

        public int SheetCount => GetSpreadsheet().Sheets.Count;


        public GoogleTable()
        {
            using var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read);
            const string credPath = "token.json";
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
            
            Logger.GetInstance().Log("Credential file saved to: " + credPath);
            
            Service = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }


        public int ColumnCount(TableIndex index)
        {
            int? count = GetSpreadsheet().Sheets[index.SheetId].Properties.GridProperties.ColumnCount;

            return count ?? throw new NonExistingIndexException();
        }

        public int RowCount(TableIndex index)
        {
            int? count = GetSpreadsheet().Sheets[index.SheetId].Properties.GridProperties.RowCount;

            return count ?? throw new NonExistingIndexException();
        }

        public void CreateSheet(TableIndex index)
        {
            BatchUpdateSpreadsheetRequest requestBody = new Request
            {
                AddSheet = new AddSheetRequest
                {
                    Properties = index.ToGoogleSheetProperties()
                }
            }.ToBody();

            Service.Spreadsheets
                .BatchUpdate(requestBody, SpreadsheetId)
                .Execute();
        }

        public void DeleteSheet(TableIndex index)
        {
            BatchUpdateSpreadsheetRequest requestBody = new Request
            {
                DeleteSheet = new DeleteSheetRequest
                {
                    SheetId = index.SheetId
                }
            }.ToBody();

            Service.Spreadsheets
                .BatchUpdate(requestBody, SpreadsheetId)
                .Execute();
        }

        public void Load(string address)
        {
            SpreadsheetId = address;
        }

        public string Create(string name)
        {
            var spreadsheet = new Spreadsheet
            {
                Properties = new SpreadsheetProperties
                {
                    Title = name
                }
            };

            return Service.Spreadsheets.Create(spreadsheet).Execute().SpreadsheetId;
        }

        public void Rename(string name)
        {
            BatchUpdateSpreadsheetRequest requestBody = new Request
            {
                UpdateSpreadsheetProperties = new UpdateSpreadsheetPropertiesRequest
                {
                    Properties = new SpreadsheetProperties
                    {
                        Title = name
                    },
                    Fields = Title
                }
            }.ToBody();

            Service.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
        }

        public void RenameSheet(TableIndex index, string name)
        {
            BatchUpdateSpreadsheetRequest requestBody = new Request
            {
                UpdateSheetProperties = new UpdateSheetPropertiesRequest
                {
                    Properties = new SheetProperties
                    {
                        SheetId = index.SheetId,
                        Title = name
                    },
                    Fields = Title
                }
            }.ToBody();

            Service.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
            index.SheetName = name;
        }

        public T GetValueForCellAt<T>(TableIndex index)
        {
            return GetValuesForCellsAt<T>(new TableIndexRange(index))[0][0];
        }

        public string GetValueForCellAt(TableIndex index)
        {
            return GetValueForCellAt<string>(index);
        }

        public List<List<T>> GetValuesForCellsAt<T>(TableIndexRange range)
        {
            ValueRange result = Service.Spreadsheets.Values
                .Get(SpreadsheetId, range.ToString())
                .Execute();

            if (result.Values.Count == 0)
                throw new NonExistingIndexException();

            return (List<List<T>>) Convert.ChangeType(
                result.Values.Select(r => r.ToList()).ToList(),
                typeof(List<List<T>>));
        }

        public List<List<string>> GetValuesForCellsAt(TableIndexRange range)
        {
            return GetValuesForCellsAt<string>(range);
        }

        public void SetValueForCellAt<T>(TableIndex index, T value)
        {
            SetValuesForCellsAt(index, new List<List<T>>
            {
                new List<T> {value}
            });
        }

        public void SetValuesForCellsAt<T>(TableIndex index, List<List<T>> values)
        {
            var body = new ValueRange
            {
                Values = values.Select(v => v.Cast<object>().ToList())
                    .Cast<IList<object>>()
                    .ToList(),
                MajorDimension = Direction.Horizontal.ToGoogleDimension()
            };

            var range = new TableIndexRange(index, index
                    .WithRow(index.Row + values.Count - 1)
                    .WithColumn(index.Column + values.Select(v => v.Count).Max() - 1));

            UpdateRequest request = Service.Spreadsheets.Values.Update(body, SpreadsheetId, range.ToString());
            request.ValueInputOption = USERENTERED;

            request.Execute();
        }

        public void FormatCellAt(TableIndex index, ICellStyle style)
        {
            FormatCellsAt(new TableIndexRange(index), style);
        }

        public void FormatCellsAt(TableIndexRange range, ICellStyle style)
        {
            BatchUpdateSpreadsheetRequest requestBody = new Request
            {
                RepeatCell = new RepeatCellRequest
                {
                    Range = range.ToGoogleGridRange(),
                    Cell = style.ToGoogleCellData(),
                    Fields = AllFields
                }
            }.ToBody();

            Service.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
        }

        public void MergeCellsAt(TableIndexRange range)
        {
            BatchUpdateSpreadsheetRequest requestBody = new Request
            {
                MergeCells = new MergeCellsRequest
                {
                    Range = range.ToGoogleGridRange(),
                    MergeType = "MERGE_ALL",
                }
            }.ToBody();

            Service.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
        }

        public void DeleteRowAt(TableIndex index)
        {
            BatchUpdateSpreadsheetRequest requestBody = new Request
            {
                DeleteRange = new DeleteRangeRequest
                {
                    Range = new GridRange
                    {
                        SheetId = index.SheetId,
                        StartRowIndex = index.Row,
                        EndRowIndex = index.Row + 1
                    },
                    ShiftDimension = Direction.Horizontal.ToGoogleDimension()
                }
            }.ToBody();

            Service.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
        }

        public void DeleteColumnAt(TableIndex index)
        {
            BatchUpdateSpreadsheetRequest requestBody = new Request
            {
                DeleteRange = new DeleteRangeRequest
                {
                    Range = new GridRange
                    {
                        SheetId = index.SheetId,
                        StartColumnIndex = index.Column,
                        EndColumnIndex = index.Column + 1
                    },
                    ShiftDimension = Direction.Vertical.ToGoogleDimension()
                }
            }.ToBody();

            Service.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
        }

        private Spreadsheet GetSpreadsheet()
        {
            return Service.Spreadsheets.Get(SpreadsheetId).Execute();
        }

        private const string AllFields =
            @"userEnteredFormat
                (
                    backgroundColor, 
                    borders 
                        (
                            top(color, style),
                            bottom(color, style), 
                            left(color, style), 
                            right(color, style)
                        ),
                    horizontalAlignment, 
                    verticalAlignment, 
                    wrapStrategy, 
                    textFormat
                        (
                            foregroundColor, 
                            fontFamily, 
                            fontSize, 
                            bold, 
                            italic, 
                            strikethrough, 
                            underline
                        ), 
                    hyperlink
                )";

        private const string Title = "title";
    }

    internal static class GoogleRequestCreator
    {
        public static BatchUpdateSpreadsheetRequest ToBody(this Request request)
            => new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request>
                {
                    request
                }
            };

        public static BatchUpdateSpreadsheetRequest ToBody(this List<Request> requests)
            => new BatchUpdateSpreadsheetRequest
            {
                Requests = requests
            };
    }
}