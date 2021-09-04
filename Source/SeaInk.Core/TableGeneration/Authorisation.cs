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
    public class Authorisation : AuthorisationBase
    {
        protected override async Task<AuthorisationService> GetAuthorisationService()
        {
            return await AuthorisationService.CreateAsync("Test Application",
                "user",
                new FileStream("Secrets.json", FileMode.Open, FileAccess.Read),
                "token.json",
                new[] {Scope.Drive, Scope.Spreadsheets});
        }

        protected override async Task<File> GetFile(AuthorisationService authorisationService)
        {
            var driveService = new DriveService(authorisationService);
            var queryCondition = QueryTerm.Name.Equal("Test Folder");
            var listActionConfiguration = new ListActionConfiguration(queryCondition);
            
            var findFiles = await driveService.FindFilesAsync(listActionConfiguration);
            var folder = (Folder) findFiles.Result.Single();
            var fileDescriptor = new FileDescriptor("Test sheet", FileType.Spreadsheet, folder);
            
            return await driveService.CreateFileAsync(fileDescriptor);
        }
        
        protected override async Task<Spreadsheet> GetSpreadsheet(AuthorisationService authorisation, File file)
        {
            var sheetsService = new SheetsService(authorisation);
            
            return await sheetsService.GetSpreadsheetAsync(file);
        }
    }
}