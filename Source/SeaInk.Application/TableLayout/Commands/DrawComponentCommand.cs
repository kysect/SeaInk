using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Commands
{
    public class DrawComponentCommand : GenericLayoutCommand<IDrawableLayoutComponent>
    {
        private readonly IDrawableLayoutComponent _component;

        public DrawComponentCommand(IDrawableLayoutComponent component)
        {
            _component = component.ThrowIfNull(nameof(component));
        }

        protected override bool TryExecute(IDrawableLayoutComponent target, ITableIndex begin, ITableEditor? editor)
        {
            if (!_component.Equals(target))
                return false;

            var index = new MergeScanningIndex(begin.Copy(), editor.ThrowIfNull(nameof(editor)));
            target.Draw(index, editor!);
            return true;
        }
    }
}