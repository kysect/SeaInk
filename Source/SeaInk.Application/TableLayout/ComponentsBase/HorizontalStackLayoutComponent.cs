using System.Collections.Generic;
using System.Linq;
using Kysect.Centum.Sheets.Indices;
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

        protected override ISheetIndex MoveIndexToNextComponent(ISheetIndex index, TComponent component)
            => index + new SheetIndex(component.Frame.Width, 0);

        protected override Scale GetScale(TComponent component)
            => new Scale(1, Frame.Height / component.Frame.Height);
    }
}