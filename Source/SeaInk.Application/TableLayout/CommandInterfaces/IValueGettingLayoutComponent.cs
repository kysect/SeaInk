using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IValueGettingLayoutComponent<T>
    {
        T GetValue(ITableIndex begin, ITableDataProvider provider);
    }
}