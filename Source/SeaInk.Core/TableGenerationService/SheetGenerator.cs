using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kysect.CentumFramework.Drive;
using Kysect.CentumFramework.Drive.Actions.Models;
using Kysect.CentumFramework.Drive.Entities;
using Kysect.CentumFramework.Drive.Models;
using Kysect.CentumFramework.Drive.Models.Query;
using Kysect.CentumFramework.Sheets;
using Kysect.CentumFramework.Sheets.Entities;
using Kysect.CentumFramework.Sheets.Models.Indices;
using Kysect.CentumFramework.Utility.Authorization;
using SeaInk.Core.Models;

namespace SeaInk.Core.TableGenerationService
{
    public class SheetGenerator
    {
        private readonly SheetConfiguration _sheetConfiguration;

        private Sheet _sheet;

        public SheetGenerator(SheetConfiguration sheetConfiguration)
        {
            _sheetConfiguration = sheetConfiguration;
        }

        public async Task AuthorizeSheet()
        {
            var authorisation = await AuthorisationService.CreateAsync("Test Application",
                "user",
                new FileStream("Secrets.json", FileMode.Open, FileAccess.Read),
                "token.json",
                new[] {Scope.Drive, Scope.Spreadsheets});

            var driveService = new DriveService(authorisation);
            var sheetsService = new SheetsService(authorisation);

            //Всё в тестовом варианте, потом это будет на сервере, нужно будет исправить разумеется
            QueryCondition condition = QueryTerm.Name.Equal("Test Folder");
            var configuration = new ListActionConfiguration(condition);
            var folder = (Folder) driveService.FindFiles(configuration).Result.Single();

            var sheetDescriptor = new FileDescriptor(_sheetConfiguration.Title, FileType.Spreadsheet, folder);
            var sheetFile = driveService.CreateFile(sheetDescriptor);
            var spreadsheet = sheetsService.GetSpreadsheet(sheetFile);
            _sheet = spreadsheet[0];
        }
        
        public void CreateSheet()
        {
            for (var i = 0; i < _sheetConfiguration.Columns.Count; i++)
            {
                _sheetConfiguration.Columns[i].Range = new SheetIndexRange
                (
                    new SheetIndex(new ColumnIndex(1 + i), new RowIndex(3))
                );
                
                var column = new ColumnGenerator(_sheetConfiguration.Columns[i], _sheet);
                column.CreateColumn();
            }
        }

        public void AddStudents(IReadOnlyList<string> students)
        {
            for (var i = 0; i < _sheetConfiguration.Columns.Count; i++)
            {
                _sheetConfiguration.Columns[i].Range = new SheetIndexRange
                (
                    new SheetIndex(new ColumnIndex(1 + i), new RowIndex(4)),
                    new SheetIndex(new ColumnIndex(1 + i), new RowIndex(4 + students.Count - 1))
                );
                
                ColumnGenerator column = new ColumnGenerator(_sheetConfiguration.Columns[i], _sheet);
                column.UpdateColumn(students);
            }
        }
    }
}