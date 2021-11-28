using System.Collections.Generic;
using System.Linq;
using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.ComponentsBase
{
    public abstract class CompositeLayoutComponent<TComponent> : LayoutComponent,
                                                                 IExpandableLayoutComponent<TComponent>,
                                                                 IReducibleLayoutComponent<TComponent>
        where TComponent : LayoutComponent
    {
        private readonly List<TComponent> _components;

        protected CompositeLayoutComponent(IReadOnlyCollection<TComponent> components)
        {
            _components = components.ThrowIfNull(nameof(components)).ToList();
        }

        public IReadOnlyCollection<TComponent> Components => _components;

        public bool TryAddComponent(TComponent component, IScaledTableIndex begin, ITableEditor editor)
        {
            if (_components.Contains(component))
                return false;

            _components.Add(component);
            return true;
        }

        public bool TryRemoveComponent(TComponent component, IScaledTableIndex begin, ITableEditor editor)
            => _components.Remove(component);

        public override bool TryExecuteCommand(ILayoutCommand command, ITableIndex begin, ITableEditor? editor)
        {
            if (base.TryExecuteCommand(command, begin, editor))
                return true;

            ITableIndex compositionIndex = begin.Copy();

            foreach (TComponent component in Components)
            {
                Scale scale = GetScale(component);
                var index = new ScaledTableIndex(scale, compositionIndex.Copy());

                if (component.TryExecuteCommand(command, index, editor))
                    return true;

                MoveIndexToNextComponent(compositionIndex, component);
            }

            return false;
        }

        public override bool Equals(LayoutComponent? other)
        {
            if (other is not CompositeLayoutComponent<TComponent> otherComposite ||
                otherComposite.Components.Count != Components.Count)
                return false;

            for (int i = 0; i < Components.Count; i++)
            {
                if (!otherComposite._components[i].Equals(_components[i]))
                    return false;
            }

            return true;
        }

        public sealed override bool Equals(object? obj)
            => Equals(obj as LayoutComponent);

        public sealed override int GetHashCode()
            => _components.GetHashCode();

        protected abstract void MoveIndexToNextComponent(ITableIndex index, TComponent component);
        protected abstract Scale GetScale(TComponent component);
    }
}