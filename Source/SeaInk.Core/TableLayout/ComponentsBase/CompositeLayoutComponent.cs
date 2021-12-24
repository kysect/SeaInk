using System.Collections.Generic;
using System.Linq;
using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.CommandInterfaces;
using SeaInk.Core.TableLayout.CommandsBase;
using SeaInk.Core.TableLayout.Errors;
using SeaInk.Core.TableLayout.Indices;
using SeaInk.Core.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.TableLayout.ComponentsBase
{
    public abstract class CompositeLayoutComponent<TComponent> : LayoutComponent,
                                                                 IExpandableLayoutComponent<TComponent>,
                                                                 IReducibleLayoutComponent<TComponent>
        where TComponent : LayoutComponent
    {
        private readonly List<TComponent> _components;

        protected CompositeLayoutComponent(IReadOnlyCollection<TComponent> components)
        {
            _components = components.ThrowIfNull().ToList();
        }

        public IReadOnlyCollection<TComponent> Components => _components.AsReadOnly();

        public Result AddComponent(TComponent component, IScaledTableIndex begin, ISheetEditor editor)
        {
            if (_components.Contains(component))
                return Result.Fail(new NotContainedComponentError(component));

            _components.Add(component);
            return Result.Ok();
        }

        public Result RemoveComponent(TComponent component, IScaledTableIndex begin, ISheetEditor editor)
            => _components.Remove(component) ? Result.Fail(new NotContainedComponentError(component)) : Result.Ok();

        public override Result ExecuteCommand(ILayoutCommand command, ISheetIndex begin, ISheetEditor? editor)
        {
            ISheetIndex compositionIndex = begin.Copy();

            foreach (TComponent component in Components)
            {
                Scale scale = GetScale(component);
                var index = new ScaledTableIndex(scale, compositionIndex.Copy());

                Result result = component.ExecuteCommand(command, index, editor);
                if (result.IsSuccess)
                    return result;

                compositionIndex = MoveIndexToNextComponent(compositionIndex, component);
            }

            return base.ExecuteCommand(command, begin, editor);
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

        protected abstract ISheetIndex MoveIndexToNextComponent(ISheetIndex index, TComponent component);
        protected abstract Scale GetScale(TComponent component);
    }
}