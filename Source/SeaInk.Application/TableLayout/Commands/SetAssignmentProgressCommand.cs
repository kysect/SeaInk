using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.Components;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Commands
{
    public class SetAssignmentProgressCommand : ILayoutCommand
    {
        private readonly AssignmentProgressModel _value;

        public SetAssignmentProgressCommand(AssignmentProgressModel value)
        {
            _value = value.ThrowIfNull(nameof(value));
        }

        public bool TryExecute(LayoutComponent target, ITableIndex begin, ITableEditor? editor)
        {
            if (target is not AssignmentColumnComponent assignmentColumnComponent)
                return false;

            if (!assignmentColumnComponent.Value.Equals(_value.Assignment))
                return false;

            assignmentColumnComponent.SetValue(_value.Progress, begin, editor.ThrowIfNull(nameof(editor)));
            return true;
        }
    }
}