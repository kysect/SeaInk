using System.Collections.Generic;
using System.Linq;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.Models;
using SeaInk.Core.Tools;

namespace SeaInk.Core.TableLayout.ComponentsBase
{
    public class VerticalStackLayoutComponent<TComponent> : CompositeLayoutComponent<TComponent>
        where TComponent : LayoutComponent
    {
        public VerticalStackLayoutComponent(IReadOnlyCollection<TComponent> components)
            : base(components) { }

        public override Frame Frame => new Frame(
            LcmCounter.Count(Components.Select(c => c.Frame.Width).ToArray()),
            Components.Sum(c => c.Frame.Height));

        protected override ISheetIndex MoveIndexToNextComponent(ISheetIndex index, TComponent component)
            => index + new SheetIndex(0, component.Frame.Height);

        protected override Scale GetScale(TComponent component)
            => new Scale(Frame.Width / component.Frame.Width, 1);
    }
}