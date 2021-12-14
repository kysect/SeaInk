using FluentResults;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Core.TableLayout.Indices;

namespace SeaInk.Core.TableLayout.CommandInterfaces
{
    public interface IExpandableLayoutComponent<in TComponent>
        where TComponent : LayoutComponent
    {
        Result AddComponent(TComponent component, IScaledTableIndex begin, ITableEditor editor);
    }
}