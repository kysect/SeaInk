using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IReducibleLayoutComponent<in TComponent>
        where TComponent : LayoutComponent
    {
        bool TryRemoveComponent(TComponent component, IScaledTableIndex begin, ITableEditor editor);
    }
}