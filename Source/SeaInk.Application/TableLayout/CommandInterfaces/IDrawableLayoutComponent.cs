using Kysect.Centum.Sheets.Indices;

namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IDrawableLayoutComponent
    {
        void Draw(ISheetIndex begin, ITableEditor editor);
    }
}