using FluentResults;
using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.Errors;
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

        protected override Result Execute(IRemovableComponent target, ITableIndex begin, ITableEditor? editor)
        {
            if (!target.Equals(_component))
                return Result.Fail(new InvalidComponentError<IRemovableComponent>(_component, target));

            target.Remove(begin, editor.ThrowIfNull(nameof(editor)));
            return Result.Ok();
        }
    }
}