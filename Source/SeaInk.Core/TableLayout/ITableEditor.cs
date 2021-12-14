using System.Collections.Generic;
using System.Threading.Tasks;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.Models;

namespace SeaInk.Core.TableLayout
{
    public interface ITableEditor
    {
        void EnqueueWrite(ISheetIndex index, IReadOnlyCollection<IReadOnlyCollection<string>> data);
        void EnqueueMerge(ISheetIndex index, Frame frame);

        void EnqueueInsertColumn(ColumnIndex column);
        void EnqueueInsertRow(RowIndex row);

        void EnqueueDeleteColumn(ColumnIndex column);
        void EnqueueDeleteRow(RowIndex row);

        Task ExecuteAsync();
    }
}