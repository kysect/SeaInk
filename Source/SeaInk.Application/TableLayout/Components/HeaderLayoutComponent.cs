using System.Collections.Generic;
using SeaInk.Application.Exceptions;
using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.Commands;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Components
{
    public class HeaderLayoutComponent : LayoutComponent,
                                         IValueGettingLayoutComponent<TableRowModel>,
                                         IValueSettingLayoutComponent<TableRowModel>
    {
        private readonly HorizontalStackLayoutComponent<LayoutComponent> _stack;

        public HeaderLayoutComponent(IReadOnlyCollection<LayoutComponent> components)
        {
            _stack = new HorizontalStackLayoutComponent<LayoutComponent>(components);
        }

        public override Frame Frame => _stack.Frame;

        public override bool Equals(LayoutComponent? other)
            => _stack.Equals(other);

        public override bool TryExecuteCommand(ILayoutCommand command, ITableIndex begin, ITableEditor? editor)
            => base.TryExecuteCommand(command, begin, editor) || _stack.TryExecuteCommand(command, begin, editor);

        public TableRowModel GetValue(ITableIndex begin, ITableDataProvider provider)
        {
            var getStudentCommand = new GetValueCommand<StudentModel>(provider);

            if (!_stack.TryExecuteCommand(getStudentCommand, begin.Copy(), null))
                throw new MissingStudentComponentException();

            var aggregateAssignmentProgressesCommand = new AggregateValuesCommand<AssignmentProgressModel>(provider);
            _stack.TryExecuteCommand(aggregateAssignmentProgressesCommand, begin.Copy(), null);

            return new TableRowModel(
                getStudentCommand.Value.ThrowIfNull(nameof(StudentModel)),
                aggregateAssignmentProgressesCommand.Values);
        }

        public void SetValue(TableRowModel value, ITableIndex begin, ITableEditor editor)
        {
            var studentSetCommand = new SetValueCommand<StudentModel>(value.Student);

            if (!_stack.TryExecuteCommand(studentSetCommand, begin.Copy(), editor))
                throw new MissingStudentComponentException();

            foreach (AssignmentProgressModel model in value.AssignmentProgresses)
            {
                var assignmentSetCommand = new SetAssignmentProgressCommand(model);

                if (!_stack.TryExecuteCommand(assignmentSetCommand, begin.Copy(), editor))
                    throw new MissingAssignmentComponentException(model.Assignment);
            }
        }

        public override int GetHashCode()
            => _stack.GetHashCode();
    }
}