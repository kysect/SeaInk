using Kysect.Centum.Sheets.Indices;

namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IRemovableComponent
    {
        void Remove(ISheetIndex begin, ITableEditor editor);
    }
}