using System.Globalization;
using Kysect.Centum.Sheets.Indices;
using Newtonsoft.Json;
using SeaInk.Core.Models;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Core.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.TableLayout.Components
{
    public class PlainAssignmentColumnComponent : AssignmentColumnComponent
    {
        public PlainAssignmentColumnComponent(AssignmentModel value)
        {
            Value = value.ThrowIfNull();
        }

        public override Frame Frame => new Frame(1, 1);

        [JsonProperty]
        public override AssignmentModel Value { get; }

        public override void Remove(ISheetIndex begin, ISheetEditor editor)
            => editor.EnqueueDeleteColumn(begin.Column);

        public override void Draw(ISheetIndex begin, ISheetEditor editor)
            => editor.EnqueueWrite(begin, new[] { new[] { Value.Title } });

        public override AssignmentProgress GetValue(ISheetIndex begin, ISheetDataProvider provider)
            => new AssignmentProgress(double.Parse(provider[begin]));

        public override void SetValue(AssignmentProgress value, ISheetIndex begin, ISheetEditor editor)
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