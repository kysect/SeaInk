using System.Collections.Generic;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Application.TableLayout.Visitors;

namespace SeaInk.Application.TableLayout.Components
{
    public class HeaderLayout : HorizontalStackLayoutComponent<HeaderLayoutComponent<ITableRowVisitor>>, IVisitedLayoutComponent<ITableVisitor>
    {
        public HeaderLayout(IReadOnlyCollection<HeaderLayoutComponent<ITableRowVisitor>> components)
            : base(components) { }

        public void SetVisit(ITableVisitor value, ITableIndex begin, ITableEditor editor)
        {
            ITableIndex index = begin.Copy();
            index.MoveVertically(Frame.Height);

            foreach (TableRowModel row in value.GetRows())
            {
                var visitor = new TableRowVisitor(row);
                ITableIndex visitingIndex = index.Copy();

                foreach (HeaderLayoutComponent<ITableRowVisitor> component in Components)
                {
                    var frame = new Frame(component.Frame.Width, 1);
                    var componentBeginIndex = new LockedTableIndex(visitingIndex.Copy(), frame);
                    component.SetVisit(visitor, componentBeginIndex, editor);
                    visitingIndex.MoveHorizontally(component.Frame.Width);
                }

                index.MoveVertically();
            }
        }

        public void GetVisit(ITableVisitor value, ITableIndex begin, ITableDataProvider provider)
        {
            ITableIndex index = begin.Copy();
            index.MoveVertically(Frame.Height);

            for (int i = 0; i < value.GetCount(); i++)
            {
                var visitor = new TableRowVisitor();
                ITableIndex visitingIndex = index.Copy();

                foreach (HeaderLayoutComponent<ITableRowVisitor> component in Components)
                {
                    var frame = new Frame(component.Frame.Width, 1);
                    var componentBeginIndex = new LockedTableIndex(visitingIndex.Copy(), frame);
                    component.GetVisit(visitor, componentBeginIndex, provider);
                    visitingIndex.MoveHorizontally(component.Frame.Width);
                }

                index.MoveVertically();
            }
        }
    }
}