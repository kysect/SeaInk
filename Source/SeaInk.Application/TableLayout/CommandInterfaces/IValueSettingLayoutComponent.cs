using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IValueSettingLayoutComponent<in T>
    {
        void SetValue(T value, ITableIndex begin, ITableEditor editor);
    }
}