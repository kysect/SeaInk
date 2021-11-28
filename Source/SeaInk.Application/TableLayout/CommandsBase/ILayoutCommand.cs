using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.CommandsBase
{
    public interface ILayoutCommand
    {
        bool TryExecute(LayoutComponent target, ITableIndex begin, ITableEditor? editor);
    }
}