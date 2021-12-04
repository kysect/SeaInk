using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout
{
    public interface ITableDataProvider
    {
        Frame Frame { get; }
        string this[ITableIndex index] { get; }
    }
}