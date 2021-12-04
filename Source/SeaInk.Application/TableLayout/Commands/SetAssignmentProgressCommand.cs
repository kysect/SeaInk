using FluentResults;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.Components;
using SeaInk.Application.TableLayout.Errors;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Commands
{
    public class SetAssignmentProgressCommand : GenericLayoutCommand<AssignmentColumnComponent>
    {
        private readonly AssignmentProgressModel _value;

        public SetAssignmentProgressCommand(AssignmentProgressModel value)
        {
            _value = value.ThrowIfNull(nameof(value));
        }

        protected override Result Execute(AssignmentColumnComponent target, ITableIndex begin, ITableEditor? editor)
        {
            if (!target.Value.Equals(_value.Assignment))
                return Result.Fail(new InvalidRepresentingValue<AssignmentModel>(_value.Assignment, target.Value));

            target.SetValue(_value.Progress, begin, editor.ThrowIfNull(nameof(editor)));
            return Result.Ok();
        }
    }
}