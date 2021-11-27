using System.Collections.Generic;
using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout
{
    public interface ITableEditor
    {
        void EnqueueWrite(ITableIndex index, IReadOnlyCollection<IReadOnlyCollection<string>> data);
    }
}