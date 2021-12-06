using System.Threading.Tasks;
using SeaInk.Application.TableLayout;

namespace SeaInk.Application.Factories
{
    public interface ITableToolsFactory
    {
        Task<ITableEditor> GetEditor(string spreadsheetId, int sheetId);
        Task<ITableDataProvider> GetDataProvider(string spreadsheetId, int sheetId);
    }
}