using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.CommandInterfaces;
using SeaInk.Core.TableLayout.CommandsBase;
using SeaInk.Core.TableLayout.Errors;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.TableLayout.Commands
{
    public class ComponentRemoveCommand : GenericLayoutCommand<IRemovableComponent>
    {
        private readonly IRemovableComponent _component;

        public ComponentRemoveCommand(IRemovableComponent component)
        {
            _component = component.ThrowIfNull();
        }

        protected override Result Execute(IRemovableComponent target, ISheetIndex begin, ISheetEditor? editor)
        {
            if (!target.Equals(_component))
                return Result.Fail(new InvalidComponentError<IRemovableComponent>(_component, target));

            target.Remove(begin, editor.ThrowIfNull());
            return Result.Ok();
        }
    }
}