using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using SeaInk.Core.Services;
using SeaInk.Core.TableIntegrations.Google.Exceptions;
using SeaInk.Core.TableIntegrations.Models;
using SeaInk.Core.TableIntegrations.Models.Exceptions;
using SeaInk.Core.TableIntegrations.Models.Styles;
using SeaInk.Core.TableIntegrations.Models.Styles.Enums;
using SeaInk.Core.Utils;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum;
using GoogleFile = Google.Apis.Drive.v3.Data.File;

namespace SeaInk.Core.TableIntegrations.Google
{
    /// <inheritdoc cref="ITable"/>
    public class GoogleTable : ITable
    {
        private const string ApplicationName = "Sea Ink";

        private string SpreadsheetId { get; set; }
        private SheetsService SheetsService { get; set; }
        private DriveService DriveService { get; set; }

        public int SheetCount => GetSpreadsheet().Sheets.Count;


        public GoogleTable(UserCredential credential)
        {
            SheetsService = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            DriveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });
        }


        public int ColumnCount(TableIndex index)
        {
            int? count = GetSpreadsheet().Sheets[index.SheetIndex.Id].Properties.GridProperties.ColumnCount;

            return count ?? throw new NonExistingIndexException();
        }

        public int RowCount(TableIndex index)
        {
            int? count = GetSpreadsheet().Sheets[index.SheetIndex.Id].Properties.GridProperties.RowCount;

            return count ?? throw new NonExistingIndexException();
        }

        public void CreateSheet(SheetIndex index)
        {
            BatchUpdateSpreadsheetRequest requestBody = new Request
            {
                AddSheet = new AddSheetRequest
                {
                    Properties = index.ToGoogleSheetProperties()
                }
            }.ToBody();

            SheetsService.Spreadsheets
                .BatchUpdate(requestBody, SpreadsheetId)
                .Execute();
        }

        public void DeleteSheet(SheetIndex index)
        {
            BatchUpdateSpreadsheetRequest requestBody = new Request
            {
                DeleteSheet = new DeleteSheetRequest
                {
                    SheetId = index.Id
                }
            }.ToBody();

            SheetsService.Spreadsheets
                .BatchUpdate(requestBody, SpreadsheetId)
                .Execute();
        }

        public void Load(TableInfo address)
        {
            SpreadsheetId = address.Id;
        }

        public string Create(TableInfo tableInfo, DrivePath path)
        {
            if (!AuthService.HasDriveAccess())
                throw new InsufficientPermissionException();

            tableInfo.Location = EstablishLocationForPath(new DrivePath(path));

            GoogleFile file = CreateFile(GoogleFileTypes.GoogleSpreadsheet, tableInfo.Name, tableInfo.Location,
                tableInfo.Description);
            SpreadsheetId = file.Id;
            tableInfo.Id = file.Id;

            return file.Id;
        }

        public string Create(TableInfo tableInfo) => Create(tableInfo, new DrivePath(new List<string>()));

        public void Delete()
        {
            FilesResource.DeleteRequest deleteRequest = DriveService.Files.Delete(SpreadsheetId);
            deleteRequest.SupportsAllDrives = true;

            deleteRequest.Execute();
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

            SheetsService.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
        }

        public void RenameSheet(SheetIndex index, string name)
        {
            BatchUpdateSpreadsheetRequest requestBody = new Request
            {
                UpdateSheetProperties = new UpdateSheetPropertiesRequest
                {
                    Properties = new SheetProperties
                    {
                        SheetId = index.Id,
                        Title = name
                    },
                    Fields = Title
                }
            }.ToBody();

            SheetsService.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
            index.Name = name;
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
            ValueRange result = SheetsService.Spreadsheets.Values
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

            UpdateRequest request = SheetsService.Spreadsheets.Values.Update(body, SpreadsheetId, range.ToString());
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

            SheetsService.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
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

            SheetsService.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
        }

        public void DeleteRowAt(TableIndex index)
        {
            BatchUpdateSpreadsheetRequest requestBody = new Request
            {
                DeleteRange = new DeleteRangeRequest
                {
                    Range = new GridRange
                    {
                        SheetId = index.SheetIndex.Id,
                        StartRowIndex = index.Row,
                        EndRowIndex = index.Row + 1
                    },
                    ShiftDimension = Direction.Horizontal.ToGoogleDimension()
                }
            }.ToBody();

            SheetsService.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
        }

        public void DeleteColumnAt(TableIndex index)
        {
            BatchUpdateSpreadsheetRequest requestBody = new Request
            {
                DeleteRange = new DeleteRangeRequest
                {
                    Range = new GridRange
                    {
                        SheetId = index.SheetIndex.Id,
                        StartColumnIndex = index.Column,
                        EndColumnIndex = index.Column + 1
                    },
                    ShiftDimension = Direction.Vertical.ToGoogleDimension()
                }
            }.ToBody();

            SheetsService.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
        }

        private Spreadsheet GetSpreadsheet()
        {
            return SheetsService.Spreadsheets.Get(SpreadsheetId).Execute();
        }

        private string EstablishLocationForPath(DrivePath path)
        {
            string location = null;

            foreach (string folder in path)
            {
                string folderId = SearchFileId(GoogleFileTypes.Folder, folder, location);

                if (folderId != null)
                    location = folderId;
                else
                {
                    location = CreateFile(GoogleFileTypes.Folder, folder, "id", location).Id;
                    Logger.Log($"Created folder named: {folder}, with id: {location}");
                }
            }

            return location;
        }

        private GoogleFile CreateFile(string type, string name, string fields, string parent = null,
            string description = null)
        {
            var file = new GoogleFile
            {
                Name = name,
                MimeType = type,
                Description = description,
            };
            if (parent != null)
            {
                file.Parents = new List<string> {parent};
            }

            FilesResource.CreateRequest request = DriveService.Files.Create(file);
            request.Fields = fields;

            return request.Execute();
        }

        private string SearchFileId(string type, string name, string location = null, bool trashed = false)
        {
            FilesResource.ListRequest searchRequest = DriveService.Files.List();
            searchRequest.Q = $"mimeType = '{type}' and trashed = {(trashed ? "true" : "false")} and name = '{name}'";
            searchRequest.Fields = "files(id)";
            if (location != null)
            {
                searchRequest.Q += $" and '{location}' in parents";
            }

            return searchRequest.Execute().Files
                .Select(f => f.Id)
                .SingleOrDefault();
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
                ),
            hyperlink";

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