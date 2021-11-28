using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IValueGettingLayoutComponent<out T>
    {
        T GetValue(ITableIndex begin, ITableDataProvider provider);
    }
}