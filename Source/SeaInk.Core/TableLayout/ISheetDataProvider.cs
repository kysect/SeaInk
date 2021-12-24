using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.Models;

namespace SeaInk.Core.TableLayout
{
    public interface ISheetDataProvider
    {
        Frame Frame { get; }
        string this[ISheetIndex index] { get; }
    }
}