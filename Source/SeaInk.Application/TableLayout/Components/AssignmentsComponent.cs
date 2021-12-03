using System.Collections.Generic;
using FluentResults;
using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.Commands;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.Components
{
    public class AssignmentsComponent : LayoutComponent,
        IExpandableLayoutComponent<AssignmentColumnComponent>,
        IReducibleLayoutComponent<AssignmentColumnComponent>
    {
        private readonly HorizontalStackLayoutComponent<AssignmentColumnComponent> _stack;

        public AssignmentsComponent(IReadOnlyCollection<AssignmentColumnComponent> components)
        {
            _stack = new HorizontalStackLayoutComponent<AssignmentColumnComponent>(components);
        }

        public override Frame Frame => _stack.Frame;

        public Result AddComponent(AssignmentColumnComponent component, IScaledTableIndex begin, ITableEditor editor)
        {
            return _stack.AddComponent(component, begin, editor);
        }

        public Result RemoveComponent(AssignmentColumnComponent component, IScaledTableIndex begin, ITableEditor editor)
        {
            Result result = _stack.ExecuteCommand(new ComponentRemoveCommand(component), begin, editor);
            return result.IsSuccess ? _stack.RemoveComponent(component, begin, editor) : result;
        }

        public override bool Equals(LayoutComponent? other)
            => other is AssignmentsComponent assignmentsComponent && assignmentsComponent._stack.Equals(_stack);

        public override bool Equals(object? obj)
            => Equals(obj as LayoutComponent);

        public override int GetHashCode()
            => _stack.GetHashCode();
    }
}