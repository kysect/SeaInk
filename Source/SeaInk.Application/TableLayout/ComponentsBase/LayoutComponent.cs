using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.ComponentsBase
{
    public abstract class LayoutComponent
    {
        public abstract Frame Frame { get; }
        public abstract void Draw(ITableIndex begin, ITableEditor editor);
    }
}