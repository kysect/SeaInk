using System.Threading;
using System.Threading.Tasks;
using SeaInk.Core.Models;
using SeaInk.Core.TableLayout;

namespace SeaInk.Core.Services
{
    public interface ISheetsService
    {
        Task<CreateSpreadsheetResponse> CreateSpreadsheetAsync(string title, CancellationToken cancellationToken);
        Task<CreateSheetResponse> CreateSheetAsync(string spreadsheetId, string title, CancellationToken cancellationToken);

        Task<ISheetEditor> GetEditorFor(SheetInfo info, CancellationToken cancellationToken);
        Task<ISheetDataProvider> GetDataProviderAsync(SheetInfo info, CancellationToken cancellationToken);

        Task<SheetLink> GetSheetLinkAsync(string spreadsheetId, int sheetId, CancellationToken cancellationToken);
    }
}