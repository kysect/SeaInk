using System.Collections.Generic;
using System.Linq;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Application.Tools;

namespace SeaInk.Application.TableLayout.ComponentsBase
{
    public class HorizontalStackLayoutComponent<TComponent> : CompositeLayoutComponent<TComponent>
        where TComponent : LayoutComponent
    {
        public HorizontalStackLayoutComponent(IReadOnlyCollection<TComponent> components)
            : base(components) { }

        public override Frame Frame => new Frame(
            Components.Sum(c => c.Frame.Width),
            LcmCounter.Count(Components.Select(c => c.Frame.Height).ToArray()));

        protected override void MoveIndexToNextComponent(ITableIndex index, TComponent component)
            => index.MoveHorizontally(component.Frame.Width);

        protected override Scale GetScale(TComponent component)
            => new Scale(1, Frame.Height / component.Frame.Height);
    }
}