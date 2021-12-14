using System.Collections.Generic;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.TableLayout.Models
{
    public class TableModel
    {
        public TableModel(IReadOnlyCollection<TableRowModel> rows)
        {
            Rows = rows.ThrowIfNull();
        }

        public IReadOnlyCollection<TableRowModel> Rows { get; }
    }
}