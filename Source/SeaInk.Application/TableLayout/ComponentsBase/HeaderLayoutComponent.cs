using System.Collections.Generic;
using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.ComponentsBase
{
    public abstract class HeaderLayoutComponent<TVisitor> : VerticalStackLayoutComponent<LayoutComponent>, IVisitedLayoutComponent<TVisitor>
    {
        protected HeaderLayoutComponent(IReadOnlyCollection<LayoutComponent> components)
            : base(components) { }

        public abstract void SetVisit(TVisitor value, ITableIndex begin, ITableEditor editor);
        public abstract void GetVisit(TVisitor value, ITableIndex begin, ITableDataProvider provider);
    }
}