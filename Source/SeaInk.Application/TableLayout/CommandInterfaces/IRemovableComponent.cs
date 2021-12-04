using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IRemovableComponent
    {
        void Remove(ITableIndex begin, ITableEditor editor);
    }
}