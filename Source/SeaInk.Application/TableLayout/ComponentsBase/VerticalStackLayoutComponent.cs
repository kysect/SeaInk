using System.Collections.Generic;
using System.Linq;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Application.Tools;

namespace SeaInk.Application.TableLayout.ComponentsBase
{
    public class VerticalStackLayoutComponent<TComponent> : CompositeLayoutComponent<TComponent>
        where TComponent : LayoutComponent
    {
        public VerticalStackLayoutComponent(IReadOnlyCollection<TComponent> components)
            : base(components) { }

        public override Frame Frame => new Frame(
            LcmCounter.Count(Components.Select(c => c.Frame.Width).ToArray()),
            Components.Sum(c => c.Frame.Height));

        protected override void MoveIndexToNextComponent(ITableIndex index, TComponent component)
            => index.MoveVertically(component.Frame.Height);

        protected override Scale GetScale(TComponent component)
            => new Scale(
                Frame.Width / component.Frame.Width,
                component.Frame.Height);
    }
}