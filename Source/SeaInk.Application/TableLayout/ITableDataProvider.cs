using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout
{
    public interface ITableDataProvider
    {
        Frame Frame { get; }
        string this[ISheetIndex index] { get; }
    }
}