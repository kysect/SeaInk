using System.Globalization;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Application.TableLayout.Visitors;
using SeaInk.Core.Models;

namespace SeaInk.Application.TableLayout.Components
{
    public class AssignmentColumnComponent : HeaderLayoutComponent<ITableRowVisitor>
    {
        public AssignmentColumnComponent(AssignmentModel assignment)
            : base(new[] { new LabelComponent(assignment.Title) })
        {
            Assignment = assignment;
        }

        public AssignmentModel Assignment { get; private init; }

        public override void SetVisit(ITableRowVisitor value, ITableIndex begin, ITableEditor editor)
        {
            AssignmentProgress? progress = value.GetProgress(Assignment);

            if (progress is null)
                return;

            editor.EnqueueWrite(begin, new[,] { { progress.Points.ToString(CultureInfo.InvariantCulture) } });
        }

        public override void GetVisit(ITableRowVisitor value, ITableIndex begin, ITableDataProvider provider)
        {
            string data = provider[begin];

            if (string.IsNullOrEmpty(data) || !double.TryParse(data, out double points))
                return;

            value.AddProgress(Assignment, new AssignmentProgress(points));
        }
    }
}