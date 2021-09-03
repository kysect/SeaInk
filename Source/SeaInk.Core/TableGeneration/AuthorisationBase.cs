using System.IO;
using System.Threading.Tasks;
using Kysect.CentumFramework.Drive;
using Kysect.CentumFramework.Drive.Actions.Models;
using Kysect.CentumFramework.Drive.Entities;
using Kysect.CentumFramework.Drive.Models;
using Kysect.CentumFramework.Drive.Models.Query;
using Kysect.CentumFramework.Sheets;
using Kysect.CentumFramework.Sheets.Entities;
using Kysect.CentumFramework.Utility.Authorization;
using File = Kysect.CentumFramework.Drive.Entities.File;

namespace SeaInk.Core.TableGeneration
{
    public abstract class AuthorisationBase
    {
        public async Task<Sheet> AuthorizeSheet()
        {
            var authorisationService = await GetAuthorisationService();
            var driveService = GetDriveService(authorisationService);
            var sheetService = GetSheetService(authorisationService);

            var queryCondition = GetQueryCondition();
            var listActionConfiguration = GetListActionConfiguration(queryCondition);
            var folder = await GetFolder(driveService, listActionConfiguration);

            var fileDescriptor = GetFileDescriptor(folder);
            var file = await GetFile(driveService, fileDescriptor);
            var spreadsheet = await GetSpreadsheet(sheetService, file);

            return spreadsheet[0];
        }

        protected abstract Task<AuthorisationService> GetAuthorisationService();

        protected abstract DriveService GetDriveService(AuthorisationService authorisation);

        protected abstract SheetsService GetSheetService(AuthorisationService authorisation);

        protected abstract QueryCondition GetQueryCondition();

        protected abstract ListActionConfiguration GetListActionConfiguration(QueryCondition queryCondition);

        protected abstract Task<Folder> GetFolder(DriveService driveService, ListActionConfiguration listActionConfiguration);

        protected abstract FileDescriptor GetFileDescriptor(Folder folder);

        protected abstract Task<File> GetFile(DriveService driveService, FileDescriptor fileDescriptor);

        protected abstract Task<Spreadsheet> GetSpreadsheet(SheetsService sheetsService, File file);
    }
}
