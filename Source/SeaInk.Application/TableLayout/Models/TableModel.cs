using System.Collections.Generic;
using System.Linq;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Models
{
    public class TableModel
    {
        public TableModel(IReadOnlyCollection<TableRowModel> rows)
        {
            Rows = rows.ThrowIfNull(nameof(rows)).ToList();
        }

        public IReadOnlyCollection<TableRowModel> Rows { get; }
    }
}