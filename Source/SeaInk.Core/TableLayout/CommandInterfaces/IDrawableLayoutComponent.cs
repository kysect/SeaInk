using Kysect.Centum.Sheets.Indices;

namespace SeaInk.Core.TableLayout.CommandInterfaces
{
    public interface IDrawableLayoutComponent
    {
        void Draw(ISheetIndex begin, ISheetEditor editor);
    }
}