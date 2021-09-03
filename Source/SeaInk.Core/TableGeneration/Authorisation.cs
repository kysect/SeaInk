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

        protected override DriveService GetDriveService(AuthorisationService authorisation)
        {
            return new DriveService(authorisation);
        }

        protected override SheetsService GetSheetService(AuthorisationService authorisation)
        {
            return new SheetsService(authorisation);
        }

        protected override QueryCondition GetQueryCondition()
        {
            return QueryTerm.Name.Equal("Test Folder");
        }

        protected override ListActionConfiguration GetListActionConfiguration(QueryCondition queryCondition)
        {
            return new ListActionConfiguration(queryCondition);
        }

        protected override async Task<Folder> GetFolder(DriveService driveService, ListActionConfiguration listActionConfiguration)
        {
            var findFiles = await driveService.FindFilesAsync(listActionConfiguration);
            return (Folder) findFiles.Result.Single();
        }

        protected override FileDescriptor GetFileDescriptor(Folder folder)
        {
            return new FileDescriptor("Test sheet", FileType.Spreadsheet, folder);
        }

        protected override async Task<File> GetFile(DriveService driveService, FileDescriptor fileDescriptor)
        {
            return await driveService.CreateFileAsync(fileDescriptor);
        }

        protected override async Task<Spreadsheet> GetSpreadsheet(SheetsService sheetsService, File file)
        {
            return await sheetsService.GetSpreadsheetAsync(file);
        }
    }
}