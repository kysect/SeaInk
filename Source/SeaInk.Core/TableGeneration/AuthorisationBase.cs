using System.Threading.Tasks;
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

            var file = await GetFile(authorisationService);
            
            var spreadsheet = await GetSpreadsheet(authorisationService, file);
            return spreadsheet[0];
        }

        protected abstract Task<AuthorisationService> GetAuthorisationService();
        
        protected abstract Task<File> GetFile(AuthorisationService authorisationService);

        protected abstract Task<Spreadsheet> GetSpreadsheet(AuthorisationService authorisationService, File file);
    }
}
