using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.CommandInterfaces;
using SeaInk.Core.TableLayout.Errors;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.TableLayout.Commands
{
    public class DrawSpecificComponentCommand : DrawComponentCommand
    {
        private readonly IDrawableLayoutComponent _component;

        public DrawSpecificComponentCommand(IDrawableLayoutComponent component)
        {
            _component = component.ThrowIfNull();
        }

        protected override Result Execute(IDrawableLayoutComponent target, ISheetIndex begin, ITableEditor? editor)
        {
            return !_component.Equals(target)
                ? Result.Fail(new InvalidComponentError<IDrawableLayoutComponent>(_component, target))
                : base.Execute(target, begin, editor);
        }
    }
}