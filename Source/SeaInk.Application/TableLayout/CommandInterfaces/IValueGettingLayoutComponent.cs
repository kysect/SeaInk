using Kysect.Centum.Sheets.Indices;

namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IValueGettingLayoutComponent<T>
    {
        T GetValue(ISheetIndex begin, ITableDataProvider provider);
    }
}