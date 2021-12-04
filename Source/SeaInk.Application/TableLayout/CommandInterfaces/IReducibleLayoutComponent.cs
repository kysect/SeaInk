using FluentResults;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IReducibleLayoutComponent<in TComponent>
        where TComponent : LayoutComponent
    {
        Result RemoveComponent(TComponent component, IScaledTableIndex begin, ITableEditor editor);
    }
}