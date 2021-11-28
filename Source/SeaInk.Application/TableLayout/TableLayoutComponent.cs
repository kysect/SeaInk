using System.Collections.Generic;
using SeaInk.Application.TableLayout.Components;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout
{
    public class TableLayoutComponent : LayoutComponent
    {
        private readonly HeaderLayoutComponent _header;

        public TableLayoutComponent(HeaderLayoutComponent header)
        {
            _header = header.ThrowIfNull(nameof(header));
        }

        public override Frame Frame => _header.Frame;

        public override bool Equals(LayoutComponent? other)
            => other is TableLayoutComponent tableLayoutComponent && tableLayoutComponent._header.Equals(_header);

        public TableModel GetTable(ITableDataProvider provider)
        {
            int startRow = Frame.Height + 1;
            var index = new TableIndex(1, startRow);
            var rows = new List<TableRowModel>();

            for (int i = startRow; i <= provider.Frame.Height; i++)
            {
                rows.Add(_header.GetValue(index.Copy(), provider));
                index.MoveVertically();
            }

            return new TableModel(rows);
        }

        public void SetTable(TableModel table, ITableEditor editor)
        {
            int startRow = Frame.Height + 1;
            var index = new TableIndex(1, startRow);

            foreach (TableRowModel row in table.Rows)
            {
                _header.SetValue(row, index.Copy(), editor);
                index.MoveVertically();
            }
        }

        public override int GetHashCode()
            => _header.GetHashCode();
    }
}