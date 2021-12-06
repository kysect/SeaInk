using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.Errors;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Commands
{
    public class DrawSpecificComponentCommand : DrawComponentCommand
    {
        private readonly IDrawableLayoutComponent _component;

        public DrawSpecificComponentCommand(IDrawableLayoutComponent component)
        {
            _component = component.ThrowIfNull(nameof(component));
        }

        protected override Result Execute(IDrawableLayoutComponent target, ISheetIndex begin, ITableEditor? editor)
        {
            return !_component.Equals(target)
                ? Result.Fail(new InvalidComponentError<IDrawableLayoutComponent>(_component, target))
                : base.Execute(target, begin, editor);
        }
    }
}