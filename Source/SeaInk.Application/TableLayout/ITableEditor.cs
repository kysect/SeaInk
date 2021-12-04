using System.Collections.Generic;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout
{
    public interface ITableEditor
    {
        void EnqueueWrite(ITableIndex index, IReadOnlyCollection<IReadOnlyCollection<string>> data);
        void EnqueueMerge(ITableIndex index, Frame frame);

        void EnqueueInsertColumn(int column);
        void EnqueueInsertRow(int row);

        void EnqueueDeleteColumn(int column);
        void EnqueueDeleteRow(int row);
    }
}