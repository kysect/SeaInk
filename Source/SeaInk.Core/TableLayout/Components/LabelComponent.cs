using Kysect.Centum.Sheets.Indices;
using Newtonsoft.Json;
using SeaInk.Core.TableLayout.CommandInterfaces;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Core.TableLayout.Models;

namespace SeaInk.Core.TableLayout.Components
{
    public class LabelComponent : LayoutComponent, IDrawableLayoutComponent
    {
        [JsonProperty]
        private readonly string _value;

        public LabelComponent(string value)
        {
            _value = value;
        }

        public override Frame Frame => new Frame(1, 1);

        public void Draw(ISheetIndex begin, ISheetEditor editor)
            => editor.EnqueueWrite(begin, new[] { new[] { _value } });

        public override bool Equals(LayoutComponent? other)
            => other is LabelComponent labelComponent && labelComponent._value.Equals(_value);

        public override bool Equals(object? obj)
            => Equals(obj as LayoutComponent);

        public override int GetHashCode()
            => _value.GetHashCode();
    }
}