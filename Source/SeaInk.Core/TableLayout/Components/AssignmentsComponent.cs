using System.Collections.Generic;
using FluentResults;
using Newtonsoft.Json;
using SeaInk.Core.TableLayout.CommandInterfaces;
using SeaInk.Core.TableLayout.Commands;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Core.TableLayout.Indices;
using SeaInk.Core.TableLayout.Models;

namespace SeaInk.Core.TableLayout.Components
{
    public class AssignmentsComponent : LayoutComponent,
        IExpandableLayoutComponent<AssignmentColumnComponent>,
        IReducibleLayoutComponent<AssignmentColumnComponent>
    {
        [JsonProperty]
        private readonly HorizontalStackLayoutComponent<AssignmentColumnComponent> _stack;

        public AssignmentsComponent(IReadOnlyCollection<AssignmentColumnComponent> components)
        {
            _stack = new HorizontalStackLayoutComponent<AssignmentColumnComponent>(components);
        }

        private AssignmentsComponent(HorizontalStackLayoutComponent<AssignmentColumnComponent> stack)
        {
            _stack = stack;
        }

        public override Frame Frame => _stack.Frame;

        public Result AddComponent(AssignmentColumnComponent component, IScaledTableIndex begin, ISheetEditor editor)
        {
            return _stack.AddComponent(component, begin, editor);
        }

        public Result RemoveComponent(AssignmentColumnComponent component, IScaledTableIndex begin, ISheetEditor editor)
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