using FluentResults;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Core.TableLayout.Indices;

namespace SeaInk.Core.TableLayout.CommandInterfaces
{
    public interface IReducibleLayoutComponent<in TComponent>
        where TComponent : LayoutComponent
    {
        Result RemoveComponent(TComponent component, IScaledTableIndex begin, ITableEditor editor);
    }
}