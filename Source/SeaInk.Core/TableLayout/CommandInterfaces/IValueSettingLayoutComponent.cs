using Kysect.Centum.Sheets.Indices;

namespace SeaInk.Core.TableLayout.CommandInterfaces
{
    public interface IValueSettingLayoutComponent<T>
    {
        void SetValue(T value, ISheetIndex begin, ISheetEditor editor);
    }
}