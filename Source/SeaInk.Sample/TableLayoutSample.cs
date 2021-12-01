using SeaInk.Application.TableLayout;
using SeaInk.Application.TableLayout.Components;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Core.Models;

namespace SeaInk.Sample
{
    internal static class TableLayoutSample
    {
        private static void Main()
        {
            var assignment1 = new AssignmentModel("Some Assignment");
            var assignment2 = new AssignmentModel("My Assignment");

            LayoutComponent[] components =
            {
                new StudentsColumnComponent(),
                new AssignmentsComponent(new[]
                {
                    new PlainAssignmentColumnComponent(assignment1),
                    new PlainAssignmentColumnComponent(assignment2),
                })
            };

            var headerLayout = new HeaderLayoutComponent(components);
            var tableLayout = new TableLayoutComponent(headerLayout);

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

            var editor = (ITableEditor)new object();
            var provider = (ITableDataProvider)new object();
            tableLayout.SetTable(tableModel, editor);

            tableModel = tableLayout.GetTable(provider);
        }
    }
}