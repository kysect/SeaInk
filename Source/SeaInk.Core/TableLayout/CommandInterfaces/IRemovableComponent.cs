using Kysect.Centum.Sheets.Indices;

namespace SeaInk.Core.TableLayout.CommandInterfaces
{
    public interface IRemovableComponent
    {
        void Remove(ISheetIndex begin, ITableEditor editor);
    }
}