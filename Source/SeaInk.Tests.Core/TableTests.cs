using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kysect.CentumFramework.Drive;
using Kysect.CentumFramework.Drive.Actions.Models;
using Kysect.CentumFramework.Drive.Entities;
using Kysect.CentumFramework.Drive.Extensions;
using Kysect.CentumFramework.Drive.Models;
using Kysect.CentumFramework.Drive.Models.Query;
using Kysect.CentumFramework.Sheets;
using Kysect.CentumFramework.Sheets.Entities;
using Kysect.CentumFramework.Sheets.Models.CellStyle;
using Kysect.CentumFramework.Sheets.Models.CellStyle.Enums;
using Kysect.CentumFramework.Sheets.Models.Indices;
using Kysect.CentumFramework.Utility.Authorization;
using NUnit.Framework;
using SeaInk.Core.TableGeneration.ColumnConfigurations;
using SeaInk.Core.TableGeneration.TableConfigurations;

namespace SeaInk.Tests.Core
{
    [TestFixture]
    public class TableTests
    {
        private Sheet _sheet;
        private List<string> _students, _labs;
        private List<DateTime> _deadLines;

        [OneTimeSetUp]
        public async Task Authorisation()
        {
            var authorisationService = await AuthorisationService.CreateAsync("Test Application",
                "user",
                new FileStream("Secrets.json", FileMode.Open, FileAccess.Read),
                "token.json",
                new[] {Scope.Drive, Scope.Spreadsheets});
            
            var driveService = new DriveService(authorisationService);
            var queryCondition = QueryTerm.Name.Equal("Test Folder");
            var listActionConfiguration = new ListActionConfiguration(queryCondition);
            
            var findFiles = await driveService.FindFilesAsync(listActionConfiguration);
            var folder = (Folder) findFiles.Result.Single();
            var fileDescriptor = new FileDescriptor("Test File", FileType.Spreadsheet, folder);
            
            var file = await driveService.CreateFileAsync(fileDescriptor);
            var sheetsService = new SheetsService(authorisationService);
            
            var spreadsheet = await sheetsService.GetSpreadsheetAsync(file);
            _sheet = spreadsheet[0];
        }

        [OneTimeSetUp]
        public void Initialization()
        {
            _students = new List<string>
                {"Ген Генадий Генадиев", "Вал Валерий Валериев", "Гош Гошев Гошевич"};
            
            _labs = new List<string>
                {"Лаб 1", "Лаб 2", "Лаб 3"};
            
            _deadLines = new List<DateTime>
                {new DateTime(2021, 09, 04), new DateTime(2021, 09, 08), new DateTime(2021, 09, 12)};
        }
        
        [Test]
        public async Task StudentsTest_CorrectStudentsList()
        {
            var start = new SheetIndex(new ColumnIndex(1), new RowIndex(3));
            
            List<ListColumnConfiguration> columns = new List<ListColumnConfiguration>
            {
                new ListColumnConfiguration("ФИО", _students)
            };

            
            var generator = new DefaultTableConfiguration(columns);
            generator.Draw(_sheet, start);
            
            
            Assert.AreEqual("ФИО", await _sheet.GetValuesAsync(start with {Row = start.Row + 1}));
            
            for (var i = 0; i < _students.Count; i++)
            {
                Assert.AreEqual(_students[i], await _sheet.GetValuesAsync(start with
                {
                    Column = start.Column, Row = start.Row + i + 2
                }));
            }
            //TODO: Сделать проверку правильного форматирования. Не нашёл метод GetRangeStyle или аналогичный

        }

        [Test]
        public async Task LabsTest_CorrectLabsList()
        {
            var start = new SheetIndex(new ColumnIndex(2), new RowIndex(3));
            
            List<ListColumnConfiguration> columns = new List<ListColumnConfiguration>();
            for (var i = 0; i < _labs.Count; i++) 
            {
                columns.Add(new ListColumnConfiguration(_labs[i], _students.Count, _deadLines[i]));
            }
            
            
            var generator = new DefaultTableConfiguration(columns);
            generator.Draw(_sheet, start);

            
            for (var i = 0; i < columns.Count; i++)
            {
                Assert.AreEqual(_deadLines[i].ToString("M"), await _sheet.GetValuesAsync(start with
                {
                    Column = start.Column + i
                }));
                Assert.AreEqual(_labs[i], await _sheet.GetValuesAsync(start with
                {
                    Row = start.Row + 1, Column = start.Column + i
                }));
            }
            //TODO: Сделать проверку правильного форматирования. Не нашёл метод GetRangeStyle или аналогичный

        }
        
        [Test]
        public async Task MilestonesTest_CorrectMilestonesList()
        {
            var start = new SheetIndex(new ColumnIndex(2 + _labs.Count), new RowIndex(3));

            List<ListColumnConfiguration> columns = new List<ListColumnConfiguration>
            {
                new ListColumnConfiguration("Рубежка", _students.Count)
                {
                    BodyCellStyle = new DefaultCellStyle
                    {
                        BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Bold))
                    }
                }
            };


            var generator = new DefaultTableConfiguration(columns);
            generator.Draw(_sheet, start);
            
            
            Assert.AreEqual("Рубежка", await _sheet.GetValuesAsync(start with {Row = start.Row + 1}));
            //TODO: Сделать проверку правильного форматирования. Не нашёл метод GetRangeStyle или аналогичный
            }
        
        [Test]
        public async Task ResultsTest_CorrectResultsList()
        {
            var tableStart = new SheetIndex(new ColumnIndex(1), new RowIndex(3));
            var columnStart = new SheetIndex(new ColumnIndex(2 + _labs.Count + 1), new RowIndex(3));
            
            List<ListColumnConfiguration> columns = new List<ListColumnConfiguration>
            {
                new ListColumnConfiguration("Итог", _students.Count, (_, i) =>
                {
                    var j = tableStart with
                    {
                        Row = i.Row,
                        Column = tableStart.Column + 1
                    };
                    var range = new SheetIndexRange(j, i - (1, 0));
                    return $"=SUM({range.ToString()[1..]})";
                })
                {
                    BodyCellStyle = new DefaultCellStyle
                    {
                        BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Bold))
                    }
                }
            };
            
            var generator = new DefaultTableConfiguration(columns);
            generator.Draw(_sheet, columnStart);
            
            
            Assert.AreEqual("Итог", await _sheet.GetValuesAsync(columnStart with {Row = columnStart.Row + 1}));
            
            for (var i = 0; i < _students.Count; i++)
            {
                Assert.AreEqual("0", await _sheet.GetValuesAsync(columnStart with
                {
                    Row = columnStart.Row + i + 2
                }));
            }
            //TODO: Сделать проверку корректности формулы ($"=SUM({range.ToString()[1..]})", например вот так $"=SUM(B{i}:E{i})")
            //Так как в таблице формула превращается в число, то вытаскивание значения через GetValues даёт число, а не строку с формулой
            //TODO: Сделать проверку правильного форматирования. Не нашёл метод GetRangeStyle или аналогичный
        }
    }
}