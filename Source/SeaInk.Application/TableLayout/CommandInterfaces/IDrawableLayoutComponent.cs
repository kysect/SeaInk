using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IDrawableLayoutComponent
    {
        void Draw(ITableIndex begin, ITableEditor editor);
    }
}