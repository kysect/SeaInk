using System.Collections.Generic;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.ComponentsBase
{
    public abstract class CompositeLayoutComponent<TComponent> : LayoutComponent
        where TComponent : LayoutComponent
    {
        protected CompositeLayoutComponent(IReadOnlyCollection<TComponent> components)
        {
            Components = components;
        }

        protected IReadOnlyCollection<TComponent> Components { get; }

        public sealed override void Draw(ITableIndex begin, ITableEditor editor)
        {
            ITableIndex compositionIndex = begin.Copy();

            foreach (TComponent component in Components)
            {
                Scale scale = GetScale(component);
                var index = new ScaledTableIndex(scale, compositionIndex.Copy());
                component.Draw(index, editor);
                MoveIndexToNextComponent(compositionIndex, component);
            }
        }

        protected abstract void MoveIndexToNextComponent(ITableIndex index, TComponent component);
        protected abstract Scale GetScale(TComponent component);
    }
}