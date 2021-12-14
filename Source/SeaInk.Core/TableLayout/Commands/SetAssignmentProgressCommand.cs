using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.CommandsBase;
using SeaInk.Core.TableLayout.Components;
using SeaInk.Core.TableLayout.Errors;
using SeaInk.Core.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.TableLayout.Commands
{
    public class SetAssignmentProgressCommand : GenericLayoutCommand<AssignmentColumnComponent>
    {
        private readonly AssignmentProgressModel _value;

        public SetAssignmentProgressCommand(AssignmentProgressModel value)
        {
            _value = value.ThrowIfNull();
        }

        protected override Result Execute(AssignmentColumnComponent target, ISheetIndex begin, ITableEditor? editor)
        {
            if (!target.Value.Equals(_value.Assignment))
                return Result.Fail(new InvalidRepresentingValue<AssignmentModel>(_value.Assignment, target.Value));

            target.SetValue(_value.Progress, begin, editor.ThrowIfNull());
            return Result.Ok();
        }
    }
}