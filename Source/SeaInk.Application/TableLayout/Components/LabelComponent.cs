using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.Components
{
    public class LabelComponent : LayoutComponent
    {
        private readonly Frame _frame;
        private readonly string _value;

        public LabelComponent(string value)
        {
            _value = value;
            _frame = new Frame(1, 1);
        }

        public override Frame Frame => _frame;

        public override void Draw(ITableIndex begin, ITableEditor editor)
            => editor.EnqueueWrite(begin, new[,] { { _value } });
    }
}