using System.Collections.Generic;
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

        public override bool Equals(LayoutComponent? other)
            => other is AssignmentsComponent assignmentsComponent && assignmentsComponent._stack.Equals(_stack);

        public bool TryAddComponent(AssignmentColumnComponent component, IScaledTableIndex begin, ITableEditor editor)
        {
            return _stack.TryAddComponent(component, begin, editor);
        }

        public bool TryRemoveComponent(AssignmentColumnComponent component, IScaledTableIndex begin, ITableEditor editor)
        {
            return _stack.TryRemoveComponent(component, begin, editor) &&
                   _stack.TryExecuteCommand(new ComponentRemoveCommand(component), begin, editor);
        }

        public override int GetHashCode()
            => _stack.GetHashCode();

        public override bool Equals(object? obj)
            => Equals(obj as LayoutComponent);
    }
}