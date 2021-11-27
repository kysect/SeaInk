using SeaInk.Application.TableLayout;
using SeaInk.Application.TableLayout.Components;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Application.TableLayout.Visitors;
using SeaInk.Core.Models;

namespace SeaInk.Sample
{
    internal static class TableLayoutSample
    {
        private static void Main()
        {
            var assignment1 = new AssignmentModel("Some Assignment");
            var assignment2 = new AssignmentModel("My Assignment");
            
            HeaderLayoutComponent<ITableRowVisitor>[] components =
            {
                new StudentsColumnComponent(),
                new AssignmentColumnComponent(assignment1),
                new AssignmentColumnComponent(assignment2),
            };

            var headerLayout = new HeaderLayout(components);
            var tableLayout = new TableLayout(headerLayout, new TableIndex(0, 0));

            TableRowModel[] rows =
            {
                new TableRowModel(new StudentModel("Name"), new[]
                {
                    new AssignmentProgressModel(assignment1, new AssignmentProgress(1)),
                }),
                new TableRowModel(new StudentModel("emaN"), new[]
                {
                    new AssignmentProgressModel(assignment2, new AssignmentProgress(2)),
                }),
            };

            var tableModel = new TableModel(rows);

            // ITableEditor editor = new ITableEditorImpl();
            // tableLayout.WriteTable(tableModel, editor);
            //
            // ITableDataProvider provider = new ITableDataProviderImpl();
            // tableModel = tableLayout.ReadTable(2, provider);
        }
    }
}