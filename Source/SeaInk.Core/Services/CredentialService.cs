using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using SeaInk.Core.Utils;

namespace SeaInk.Core.Services
{
    public class CredentialService
    {
        private static readonly string[] Scopes =
        {
            SheetsService.Scope.Spreadsheets,
            DriveService.Scope.Drive
        };

        public async Task<UserCredential> GetGoogleCredentials()
        {
            await using var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read);
            const string credPath = "token.json";
            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true));

            Logger.Log("Credential file saved to: " + credPath);

            return credential;
        }
    }
}