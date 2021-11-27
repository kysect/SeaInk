using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout
{
    public interface ITableDataProvider
    {
        string this[ITableIndex index] { get; }
    }
}