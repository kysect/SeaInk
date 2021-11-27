using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.ComponentsBase
{
    public interface IVisitedLayoutComponent<in TVisitor>
    {
        void SetVisit(TVisitor value, ITableIndex begin, ITableEditor editor);
        void GetVisit(TVisitor value, ITableIndex begin, ITableDataProvider provider);
    }
}