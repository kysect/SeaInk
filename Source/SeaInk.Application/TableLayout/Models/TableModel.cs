using System.Collections.Generic;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Models
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