using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout
{
    public interface ITableEditor
    {
        void EnqueueWrite(ITableIndex index, string[,] data);
    }
}