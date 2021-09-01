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
using Kysect.CentumFramework.Utility.Authorization;

namespace SeaInk.Core.TableGeneration
{
    public class Authorisation
    {
        public static async Task<Sheet> AuthorizeSheet()
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

            var sheetDescriptor = new FileDescriptor("Test sheet", FileType.Spreadsheet, folder);
            var sheetFile = driveService.CreateFile(sheetDescriptor);
            var spreadsheet = sheetsService.GetSpreadsheet(sheetFile);
            return spreadsheet[0];
        }
    }
}