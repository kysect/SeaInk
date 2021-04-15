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
using SeaInk.Core.Models.Tables.Exceptions;
using SeaInk.Core.Models.Tables.Enums;
using LineStyle = SeaInk.Core.Models.Tables.Enums.LineStyle;

namespace SeaInk.Core.Entities.Tables
{
    /// <inheritdoc cref="ITable"/>
    public class GoogleTable : ITable
    {
        private static readonly string[] Scopes = {SheetsService.Scope.Spreadsheets};
        private const string ApplicationName = "Sea Ink";

        private string SpreadsheetId { get; set; } = "";
        private SheetsService Service { get; set; }

        public int SheetCount => GetSpreadsheet().Sheets.Count;


        public int ColumnCount(TableIndex index)
        {
            int? count = GetSpreadsheet().Sheets[index.SheetId].Properties.GridProperties.ColumnCount;

            if (!count.HasValue)
                throw new NonExistingIndexException();

            return count.Value;
        }

        public int RowCount(TableIndex index)
        {
            int? count = GetSpreadsheet().Sheets[index.SheetId].Properties.GridProperties.RowCount;

            if (!count.HasValue)
                throw new NonExistingIndexException();

            return count.Value;
        }

        public void CreateSheet(TableIndex index)
        {
            var requestBody = new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request>
                {
                    new()
                    {
                        AddSheet = new AddSheetRequest
                        {
                            Properties = new SheetProperties
                            {
                                Title = index.SheetName,
                                SheetId = index.SheetId,
                                Index = index.SheetId
                            }
                        }
                    }
                }
            };

            Service.Spreadsheets
                .BatchUpdate(requestBody, SpreadsheetId)
                .Execute();
        }

        public void DeleteSheet(TableIndex index)
        {
            var requestBody = new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request>
                {
                    new()
                    {
                        DeleteSheet = new DeleteSheetRequest
                        {
                            SheetId = index.SheetId
                        }
                    }
                }
            };

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

        public void Save()
        {
        }

        public void Rename(string name)
        {
            var requestBody = new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request>
                {
                    new()
                    {
                        UpdateSpreadsheetProperties = new UpdateSpreadsheetPropertiesRequest
                        {
                            Properties = new SpreadsheetProperties
                            {
                                Title = name
                            },
                            Fields = "title"
                        }
                    }
                }
            };

            Service.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
        }

        public void RenameSheet(TableIndex index, string name)
        {
            var requestBody = new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request>
                {
                    new()
                    {
                        UpdateSheetProperties = new UpdateSheetPropertiesRequest
                        {
                            Properties = new SheetProperties
                            {
                                SheetId = index.SheetId,
                                Title = name
                            },
                            Fields = "title"
                        }
                    }
                }
            };

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
                new()
                {
                    value
                }
            });
        }

        public void SetValuesForCellsAt<T>(TableIndex index, List<List<T>> values)
        {
            var body = new ValueRange
            {
                Values = values.Select(v => v.Cast<object>().ToList())
                    .Cast<IList<object>>()
                    .ToList(),
                MajorDimension = "ROWS"
            };

            var range = new TableIndexRange(
                index,
                index
                    .WithRow(index.Row + values.Count - 1)
                    .WithColumn(index.Column + values.Select(v => v.Count).Max() - 1));

            SpreadsheetsResource.ValuesResource.UpdateRequest request = Service.Spreadsheets.Values.Update(
                body,
                SpreadsheetId,
                range.ToString());
            request.ValueInputOption =
                SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

            request.Execute();
        }

        public void FormatCellAt(TableIndex index, ICellStyle style)
        {
            FormatCellsAt(new TableIndexRange(index), style);
        }

        public void FormatCellsAt(TableIndexRange range, ICellStyle style)
        {
            var requestBody = new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request>
                {
                    new ()
                    {
                        RepeatCell = new RepeatCellRequest
                        {
                            Range = new GridRange
                            {
                                SheetId = range.SheetId,
                                StartColumnIndex = range.From.Column,
                                StartRowIndex = range.From.Row,
                                EndColumnIndex = range.To.Column + 1,
                                EndRowIndex = range.To.Row + 1
                            },
                            Cell = new CellData
                            {
                                UserEnteredFormat = new CellFormat
                                {
                                    BackgroundColor = GoogleColorFromColor(style.BackgroundColor),
                                    Borders = new Borders
                                    {
                                        Top = new Border
                                        {
                                            Color = GoogleColorFromColor(style.BorderStyle.Top.Color),
                                            Style = LineStyleToString(style.BorderStyle.Top.Style),
                                        },
                                        Bottom = new Border
                                        {
                                            Color = GoogleColorFromColor(style.BorderStyle.Bottom.Color),
                                            Style = LineStyleToString(style.BorderStyle.Bottom.Style),
                                        },
                                        Left = new Border
                                        {
                                            Color = GoogleColorFromColor(style.BorderStyle.Leading.Color),
                                            Style = LineStyleToString(style.BorderStyle.Leading.Style),
                                        },
                                        Right = new Border
                                        {
                                            Color = GoogleColorFromColor(style.BorderStyle.Trailing.Color),
                                            Style = LineStyleToString(style.BorderStyle.Trailing.Style),
                                        }
                                    },
                                    HorizontalAlignment = HorizontalAlignmentToString(style.HorizontalAlignment),
                                    VerticalAlignment = VerticalAlignmentToString(style.VerticalAlignment),
                                    WrapStrategy = TextWrappingToString(style.TextWrapping),
                                    TextFormat = new TextFormat
                                    {
                                        ForegroundColor = GoogleColorFromColor(style.FontColor),
                                        FontFamily = style.FontName,
                                        FontSize = style.FontSize,
                                        Bold = style.FontStyle == FontStyle.Bold,
                                        Italic = style.FontStyle == FontStyle.Italic,
                                        Strikethrough = style.FontStyle == FontStyle.Crossed,
                                        Underline = style.FontStyle == FontStyle.Underlined
                                    }
                                },
                                Hyperlink = style.HyperLink
                            },
                            Fields =
                                "userEnteredFormat" +
                                "(" +
                                    "backgroundColor, " +
                                    "borders" +
                                        "(" +
                                            "top(color, style), " +
                                            "bottom(color, style), " +
                                            "left(color, style), " +
                                            "right(color, style)" +
                                        ")," +
                                    "horizontalAlignment, " +
                                    "verticalAlignment, " +
                                    "wrapStrategy, " +
                                    "textFormat" +
                                        "(" +
                                            "foregroundColor, " + 
                                            "fontFamily, " + 
                                            "fontSize, " + 
                                            "bold, " + 
                                            "italic, " +
                                            "strikethrough, " +
                                            "underline" +
                                        ")" +
                                ")," +
                                "hyperlink"
                        }
                    }
                }
            };

            Service.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
        }

        public void MergeCellsAt(TableIndexRange range)
        {
            var requestBody = new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request>
                {
                    new()
                    {
                        MergeCells = new MergeCellsRequest
                        {
                            Range = new GridRange
                            {
                                SheetId = range.SheetId,
                                StartColumnIndex = range.From.Column,
                                StartRowIndex = range.From.Row,
                                EndColumnIndex = range.To.Column + 1,
                                EndRowIndex = range.To.Row + 1
                            },
                            MergeType = "MERGE_ALL",
                        }
                    }
                }
            };

            Service.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
        }

        public void DeleteRowAt(TableIndex index)
        {
            var requestBody = new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request>
                {
                    new()
                    {
                        DeleteRange = new DeleteRangeRequest
                        {
                            Range = new GridRange
                            {
                                SheetId = index.SheetId,
                                StartRowIndex = index.Row,
                                EndRowIndex = index.Row + 1
                            },
                            ShiftDimension = "ROWS"
                        }
                    }
                }
            };

            Service.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
        }

        public void DeleteColumnAt(TableIndex index)
        {
            var requestBody = new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request>
                {
                    new()
                    {
                        DeleteRange = new DeleteRangeRequest
                        {
                            Range = new GridRange
                            {
                                SheetId = index.SheetId,
                                StartColumnIndex = index.Column,
                                EndColumnIndex = index.Column + 1
                            },
                            ShiftDimension = "COLUMNS"
                        }
                    }
                }
            };

            Service.Spreadsheets.BatchUpdate(requestBody, SpreadsheetId).Execute();
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

        private static string LineStyleToString(LineStyle style)
            => style switch
            {
                LineStyle.None => "NONE",
                LineStyle.Light => "SOLID",
                LineStyle.Bold => "SOLID_MEDIUM",
                LineStyle.Black => "SOLID_THICK",
                LineStyle.Dashed => "DASHED",
                LineStyle.Dotted => "DOTTED",
                LineStyle.Doubled => "DOUBLE",
                _ => throw new ArgumentOutOfRangeException(nameof(style), style, null)
            };

        private static string HorizontalAlignmentToString(Alignment alignment)
            => alignment switch
            {
                Alignment.Leading => "LEFT",
                Alignment.Center => "CENTER",
                Alignment.Trailing => "RIGHT",
                _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
            };

        private static string VerticalAlignmentToString(Alignment alignment)
            => alignment switch
            {
                Alignment.Top => "TOP",
                Alignment.Center => "MIDDLE",
                Alignment.Bottom => "BOTTOM",
                _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
            };

        private static string TextWrappingToString(TextWrapping wrapping)
            => wrapping switch
            {
                TextWrapping.Overlay => "OVERFLOW_CELL",
                TextWrapping.NewLine => "WRAP",
                TextWrapping.Cut => "CLIP",
                _ => throw new ArgumentOutOfRangeException(nameof(wrapping), wrapping, null)
            };

        private static Color GoogleColorFromColor(System.Drawing.Color color)
            => new()
            {
                Alpha = color.A * 255,
                Red = color.R * 255,
                Green = color.G * 255,
                Blue = color.B * 255
            };
    }
}