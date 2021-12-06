using Kysect.Centum.Sheets.Indices;

namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IValueSettingLayoutComponent<T>
    {
        void SetValue(T value, ISheetIndex begin, ITableEditor editor);
    }
}