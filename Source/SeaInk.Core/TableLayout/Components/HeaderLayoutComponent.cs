using System.Collections.Generic;
using FluentResults;
using Kysect.Centum.Sheets.Indices;
using Newtonsoft.Json;
using SeaInk.Core.TableLayout.CommandInterfaces;
using SeaInk.Core.TableLayout.Commands;
using SeaInk.Core.TableLayout.CommandsBase;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Core.TableLayout.Exceptions;
using SeaInk.Core.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.TableLayout.Components
{
    public class HeaderLayoutComponent : LayoutComponent,
                                         IValueGettingLayoutComponent<TableRowModel>,
                                         IValueSettingLayoutComponent<TableRowModel>
    {
        [JsonProperty]
        private readonly HorizontalStackLayoutComponent<LayoutComponent> _stack;

        public HeaderLayoutComponent(IReadOnlyCollection<LayoutComponent> components)
        {
            _stack = new HorizontalStackLayoutComponent<LayoutComponent>(components);
        }

        private HeaderLayoutComponent(HorizontalStackLayoutComponent<LayoutComponent> stack)
        {
            _stack = stack;
        }

        public override Frame Frame => _stack.Frame;

        public override bool Equals(LayoutComponent? other)
            => _stack.Equals(other);

        public override bool Equals(object? obj)
            => Equals(obj as LayoutComponent);

        public override Result ExecuteCommand(ILayoutCommand command, ISheetIndex begin, ISheetEditor? editor)
        {
            Result baseResult = base.ExecuteCommand(command, begin, editor);
            return baseResult.IsSuccess ? baseResult : _stack.ExecuteCommand(command, begin, editor);
        }

        public TableRowModel GetValue(ISheetIndex begin, ISheetDataProvider provider)
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

        public void SetValue(TableRowModel value, ISheetIndex begin, ISheetEditor editor)
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