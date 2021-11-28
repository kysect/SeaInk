using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.Components
{
    public class LabelComponent : LayoutComponent, IDrawableLayoutComponent
    {
        private readonly string _value;

        public LabelComponent(string value)
        {
            _value = value;
        }

        public override Frame Frame => new Frame(1, 1);

        public void Draw(ITableIndex begin, ITableEditor editor)
            => editor.EnqueueWrite(begin, new[] { new[] { _value } });

        public override bool Equals(LayoutComponent? other)
            => other is LabelComponent labelComponent && labelComponent._value.Equals(_value);

        public override int GetHashCode()
            => _value.GetHashCode();

        public override bool Equals(object? obj)
            => Equals(obj as LayoutComponent);
    }
}