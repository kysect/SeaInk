using System.Globalization;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Core.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Components
{
    public class PlainAssignmentColumnComponent : AssignmentColumnComponent
    {
        public PlainAssignmentColumnComponent(AssignmentModel value)
        {
            Value = value.ThrowIfNull(nameof(value));
        }

        public override Frame Frame => new Frame(1, 1);

        public override AssignmentModel Value { get; }

        public override void Remove(ITableIndex begin, ITableEditor editor)
            => editor.EnqueueDeleteColumn(begin.Column);

        public override void Draw(ITableIndex begin, ITableEditor editor)
            => editor.EnqueueWrite(begin, new[] { new[] { Value.Title } });

        public override AssignmentProgress GetValue(ITableIndex begin, ITableDataProvider provider)
            => new AssignmentProgress(double.Parse(provider[begin]));

        public override void SetValue(AssignmentProgress value, ITableIndex begin, ITableEditor editor)
            => editor.EnqueueWrite(begin, new[] { new[] { value.Points.ToString(CultureInfo.InvariantCulture) } });

        public override bool Equals(LayoutComponent? other)
            => other is PlainAssignmentColumnComponent plainAssignmentColumnComponent &&
               plainAssignmentColumnComponent.Value.Equals(Value);

        public override bool Equals(object? obj)
            => Equals(obj as LayoutComponent);

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}