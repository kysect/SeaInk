using System.Collections.Generic;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.Visitors
{
    public interface ITableVisitor
    {
        IReadOnlyCollection<TableRowModel> GetRows();

        int GetCount();
        void AddRow(TableRowModel row);
    }
}