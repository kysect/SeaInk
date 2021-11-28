using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.CommandsBase
{
    public abstract class GenericLayoutCommand<T> : ILayoutCommand
    {
        public bool TryExecute(LayoutComponent target, ITableIndex begin, ITableEditor? editor)
            => target is T typedTarget && TryExecute(typedTarget, begin, editor);

        protected abstract bool TryExecute(T target, ITableIndex begin, ITableEditor? editor);
    }
}