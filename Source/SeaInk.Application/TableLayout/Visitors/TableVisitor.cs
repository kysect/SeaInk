using System.Collections.Generic;
using System.Linq;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Visitors
{
    public class TableVisitor : ITableVisitor
    {
        private readonly List<TableRowModel> _rows;
        private readonly int _count;

        public TableVisitor(IReadOnlyCollection<TableRowModel> rows)
        {
            _rows = rows.ThrowIfNull(nameof(rows)).ToList();
            _count = rows.Count;
        }

        public TableVisitor(int count)
        {
            _rows = new List<TableRowModel>();
            _count = count;
        }

        public TableModel GetTableModel()
            => new TableModel(_rows);

        public IReadOnlyCollection<TableRowModel> GetRows()
            => _rows;

        public int GetCount()
            => _count;

        public void AddRow(TableRowModel row)
            => _rows.Add(row);
    }
}