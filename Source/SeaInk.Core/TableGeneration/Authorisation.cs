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
using Kysect.CentumFramework.Utility.Authorization;
using File = Kysect.CentumFramework.Drive.Entities.File;

namespace SeaInk.Core.TableGeneration
{
    public class Authorisation
    {
        public async Task<AuthorisationService> GetAuthorisationService()
        {
            return await AuthorisationService.CreateAsync("Test Application",
                "user",
                new FileStream("Secrets.json", FileMode.Open, FileAccess.Read),
                "token.json",
                new[] {Scope.Drive, Scope.Spreadsheets});
        }

        public async Task<File> GetFile(AuthorisationService authorisationService, string folderName, string fileName)
        {
            var driveService = new DriveService(authorisationService);
            var queryCondition = QueryTerm.Name.Equal(folderName);
            var listActionConfiguration = new ListActionConfiguration(queryCondition);
            
            var findFiles = await driveService.FindFilesAsync(listActionConfiguration);
            var folder = (Folder) findFiles.Result.Single();
            var fileDescriptor = new FileDescriptor(fileName, FileType.Spreadsheet, folder);
            
            return await driveService.CreateFileAsync(fileDescriptor);
        }
        
        public async Task<Sheet> GetSheet(AuthorisationService authorisationService, File file, int sheetNumber)
        {
            var sheetsService = new SheetsService(authorisationService);
            
            var spreadsheet = await sheetsService.GetSpreadsheetAsync(file);
            return spreadsheet[sheetNumber];
        }
    }
}