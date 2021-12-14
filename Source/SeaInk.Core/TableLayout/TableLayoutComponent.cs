using System.Collections.Generic;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.Components;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Core.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.TableLayout
{
    public class TableLayoutComponent : LayoutComponent
    {
        private readonly HeaderLayoutComponent _header;

        public TableLayoutComponent(HeaderLayoutComponent header)
        {
            _header = header.ThrowIfNull();
        }

#pragma warning disable CS8618
        private TableLayoutComponent() { }
#pragma warning restore CS8618

        public override Frame Frame => _header.Frame;

        public TableModel GetTable(ITableDataProvider provider)
        {
            int startRow = Frame.Height + 1;
            ISheetIndex index = new SheetIndex(1, startRow);
            var rows = new List<TableRowModel>();

            for (int i = startRow; i <= provider.Frame.Height; i++)
            {
                rows.Add(_header.GetValue(index.Copy(), provider));
                index += new SheetIndex(0, 1);
            }

            return new TableModel(rows);
        }

        public void SetTable(TableModel table, ITableEditor editor)
        {
            int startRow = Frame.Height + 1;
            ISheetIndex index = new SheetIndex(1, startRow);

            foreach (TableRowModel row in table.Rows)
            {
                _header.SetValue(row, index.Copy(), editor);
                index += new SheetIndex(0, 1);
            }
        }

        public override bool Equals(LayoutComponent? other)
            => other is TableLayoutComponent tableLayoutComponent && tableLayoutComponent._header.Equals(_header);

        public override bool Equals(object? obj)
            => Equals(obj as LayoutComponent);

        public override int GetHashCode()
            => _header.GetHashCode();
    }
}