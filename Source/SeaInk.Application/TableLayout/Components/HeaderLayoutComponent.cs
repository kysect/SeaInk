using System.Collections.Generic;
using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.Exceptions;
using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.Commands;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.ComponentsBase;
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

        public override bool Equals(object? obj)
            => Equals(obj as LayoutComponent);

        public override Result ExecuteCommand(ILayoutCommand command, ISheetIndex begin, ITableEditor? editor)
        {
            Result baseResult = base.ExecuteCommand(command, begin, editor);
            return baseResult.IsSuccess ? baseResult : _stack.ExecuteCommand(command, begin, editor);
        }

        public TableRowModel GetValue(ISheetIndex begin, ITableDataProvider provider)
        {
            var getStudentCommand = new GetValueCommand<StudentModel>(provider);

            if (!_stack.ExecuteCommand(getStudentCommand, begin.Copy(), null).IsSuccess)
                throw new MissingStudentComponentException();

            var aggregateAssignmentProgressesCommand = new AggregateValuesCommand<AssignmentProgressModel>(provider);
            _stack.ExecuteCommand(aggregateAssignmentProgressesCommand, begin.Copy(), null);

            return new TableRowModel(
                getStudentCommand.Value.ThrowIfNull(),
                aggregateAssignmentProgressesCommand.Values);
        }

        public void SetValue(TableRowModel value, ISheetIndex begin, ITableEditor editor)
        {
            var studentSetCommand = new SetValueCommand<StudentModel>(value.Student);

            if (!_stack.ExecuteCommand(studentSetCommand, begin.Copy(), editor).IsSuccess)
                throw new MissingStudentComponentException();

            foreach (AssignmentProgressModel model in value.AssignmentProgresses)
            {
                var assignmentSetCommand = new SetAssignmentProgressCommand(model);

                if (!_stack.ExecuteCommand(assignmentSetCommand, begin.Copy(), editor).IsSuccess)
                    throw new MissingAssignmentComponentException(model.Assignment);
            }
        }

        public override int GetHashCode()
            => _stack.GetHashCode();
    }
}