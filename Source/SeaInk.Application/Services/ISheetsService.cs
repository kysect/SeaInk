using System.Threading.Tasks;
using SeaInk.Application.Models;
using SeaInk.Application.TableLayout;

namespace SeaInk.Application.Services
{
    public interface ISheetsService
    {
        Task<CreateSpreadsheetResponse> CreateSpreadsheetAsync(string title);
        Task<CreateSheetResponse> CreateSheetAsync(string spreadsheetId, string title);

        ITableEditor GetEditorFor(SheetInfo info);
        Task<ITableDataProvider> GetDataProviderAsync(SheetInfo info);
    }
}