using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IExpandableLayoutComponent<in TComponent>
        where TComponent : LayoutComponent
    {
        bool TryAddComponent(TComponent component, IScaledTableIndex begin, ITableEditor editor);
    }
}