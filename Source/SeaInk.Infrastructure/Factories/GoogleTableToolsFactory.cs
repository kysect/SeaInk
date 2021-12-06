using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using SeaInk.Application.Factories;
using SeaInk.Application.TableLayout;
using SeaInk.Infrastructure.TableLayout;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.Factories
{
    public class GoogleTableToolsFactory : ITableToolsFactory
    {
        private readonly SheetsService _service;

        public GoogleTableToolsFactory(SheetsService service)
        {
            _service = service.ThrowIfNull();
        }

        public Task<ITableEditor> GetEditor(string spreadsheetId, int sheetId)
            => Task.FromResult((ITableEditor)new GoogleSheetsTableEditor(_service, spreadsheetId, sheetId));

        public Task<ITableDataProvider> GetDataProvider(string spreadsheetId, int sheetId)
            => GoogleTableDataProvider.Create(_service, spreadsheetId, sheetId);
    }
}