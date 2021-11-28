using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Commands
{
    public class ComponentRemoveCommand : GenericLayoutCommand<IRemovableComponent>
    {
        private readonly IRemovableComponent _component;

        public ComponentRemoveCommand(IRemovableComponent component)
        {
            _component = component.ThrowIfNull(nameof(component));
        }

        protected override bool TryExecute(IRemovableComponent target, ITableIndex begin, ITableEditor? editor)
        {
            if (!target.Equals(_component))
                return false;

            target.Remove(begin, editor.ThrowIfNull(nameof(editor)));
            return true;
        }
    }
}