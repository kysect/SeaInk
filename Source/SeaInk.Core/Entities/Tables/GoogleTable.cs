using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using SeaInk.Core.Models;
using SeaInk.Core.Models.Tables;
using SeaInk.Core.Models.Tables.Enums;

namespace SeaInk.Core.Entities.Tables
{
    public class GoogleTable : ITable
    {
        static string[] Scopes = {SheetsService.Scope.Spreadsheets};
        static string ApplicationName = "Google Sheets API .NET Quickstart";

        public string SpreadsheetId { get; private set; } = "";
        private SheetsService Service { get; set; }

        public int SheetCount => GetSpreadsheet().Sheets.Count;


        public int ColumnCount(TableIndex index)
        {
            return GetSpreadsheet().Sheets[index.SheetId].Properties.GridProperties
                .ColumnCount ?? 0;
        }

        public int RowCount(TableIndex index)
        {
            return GetSpreadsheet().Sheets[index.SheetId].Properties.GridProperties
                .ColumnCount ?? 0;
        }

        public void CreateSheet(TableIndex index)
        {
            var body = new Request()
            {
                AddSheet = new AddSheetRequest()
                {
                    Properties = new SheetProperties()
                    {
                        Title = index.SheetName,
                        SheetId = index.SheetId,
                        Index = index.SheetId
                    }
                }
            };

            var requestBody = new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request> {body}
            };

            BatchUpdateSpreadsheetResponse response =
                Service.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
            Console.WriteLine(response);
        }

        public void DeleteSheet(TableIndex index)
        {
            var body = new Request()
            {
                DeleteSheet = new DeleteSheetRequest()
                {
                    SheetId = index.SheetId
                }
            };

            var requestBody = new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request> {body}
            };

            BatchUpdateSpreadsheetResponse response =
                Service.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
            Console.WriteLine(response);
        }

        public void Load(string address)
        {
            SpreadsheetId = address;
        }

        public string Create(string name)
        {
            var spreadsheet = new Spreadsheet()
            {
                Properties = new SpreadsheetProperties()
                {
                    Title = name
                }
            };

            Spreadsheet response = Service.Spreadsheets.Create(spreadsheet).Execute();

            return response.SpreadsheetId;
        }

        public void Delete()
        {
        }

        public void Save()
        {
        }

        public T GetValueForCellAt<T>(TableIndex index)
        {
            ValueRange result = Service.Spreadsheets.Values.Get(SpreadsheetId, index.String).Execute();

            if (result.Values.Count == 0)
                throw new Exception("Cell does not exist");

            return (T) Convert.ChangeType(result.Values[0][0], typeof(T));
        }

        public string GetValueForCellAt(TableIndex index)
        {
            return GetValueForCellAt<string>(index);
        }

        public List<List<T>> GetValuesForCellsAt<T>(TableIndexRange range)
        {
            ValueRange result = Service.Spreadsheets.Values.Get(SpreadsheetId, range.String).Execute();

            if (result.Values.Count == 0)
                throw new Exception("Range does not exist");

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
            var body = new ValueRange
            {
                Values = new List<IList<object>>
                {
                    new List<object>
                    {
                        value
                    }
                }
            };

            SpreadsheetsResource.ValuesResource.UpdateRequest? request =
                Service.Spreadsheets.Values.Update(body, SpreadsheetId, index.String);

            request.ValueInputOption =
                SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            request.ResponseDateTimeRenderOption = SpreadsheetsResource.ValuesResource.UpdateRequest
                .ResponseDateTimeRenderOptionEnum.FORMATTEDSTRING;

            request.Execute();
        }

        /// <summary>
        /// values must be rectangular two-dimensional List
        /// </summary>
        /// <param name="index"></param>
        /// <param name="values"></param>
        public void SetValuesForCellsAt(TableIndex index, List<IList<object>> values)
        {
            var body = new ValueRange
            {
                Values = values,
                MajorDimension = "ROWS"
            };


            var range = new TableIndexRange(
                index,
                index
                    .WithRow(index.Row + values.Count - 1)
                    .WithColumn(index.Column + values[0].Count - 1));

            SpreadsheetsResource.ValuesResource.UpdateRequest? request = Service.Spreadsheets.Values.Update(body,
                SpreadsheetId,
                range.String);


            request.ValueInputOption =
                SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

            Console.WriteLine(JsonConvert.SerializeObject(request.Execute()));
        }

        public void FormatSheet(ISheetMarkup markup, TableIndex sheet)
        {
            throw new NotImplementedException();
        }

        public void FormatSheets(ISheetMarkup markup, TableIndex[]? sheets = null)
        {
            sheets ??= GetSpreadsheet().Sheets
                .Select(s => new TableIndex(s.Properties.Title, s.Properties.SheetId ?? -1)).ToArray();

            foreach (TableIndex index in sheets)
            {
                FormatSheet(markup, index);
            }
        }

        public GoogleTable()
        {
            UserCredential credential;
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            Service = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        private Spreadsheet GetSpreadsheet()
        {
            return Service.Spreadsheets.Get(SpreadsheetId).Execute();
        }
    }
}