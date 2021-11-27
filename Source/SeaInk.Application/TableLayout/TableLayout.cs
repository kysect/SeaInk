using SeaInk.Application.TableLayout.Components;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Application.TableLayout.Visitors;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout
{
    public class TableLayout
    {
        private readonly HeaderLayout _header;
        private readonly TableIndex _begin;

        public TableLayout(HeaderLayout header, TableIndex begin)
        {
            _header = header.ThrowIfNull(nameof(header));
            _begin = begin.ThrowIfNull(nameof(begin));
        }

        public void WriteTable(TableModel model, ITableEditor editor)
        {
            var visitor = new TableVisitor(model.Rows);
            _header.SetVisit(visitor, _begin, editor);
        }

        public TableModel ReadTable(int studentCount, ITableDataProvider provider)
        {
            var visitor = new TableVisitor(studentCount);
            _header.GetVisit(visitor, _begin, provider);
            return visitor.GetTableModel();
        }
    }
}