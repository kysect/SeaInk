using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.Models;

namespace SeaInk.Core.TableLayout
{
    public interface ITableDataProvider
    {
        Frame Frame { get; }
        string this[ISheetIndex index] { get; }
    }
}