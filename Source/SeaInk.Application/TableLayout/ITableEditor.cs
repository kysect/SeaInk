using System.Collections.Generic;
using System.Threading.Tasks;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout
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