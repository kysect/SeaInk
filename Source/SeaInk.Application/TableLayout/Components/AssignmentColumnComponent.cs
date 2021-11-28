using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Core.Models;

namespace SeaInk.Application.TableLayout.Components
{
    public abstract class AssignmentColumnComponent : LayoutComponent,
                                                      IDrawableLayoutComponent,
                                                      IRemovableComponent,
                                                      IValueRepresentingLayoutComponent<AssignmentModel>,
                                                      IValueGettingLayoutComponent<AssignmentProgress>,
                                                      IValueSettingLayoutComponent<AssignmentProgress>
    {
        public abstract AssignmentModel Value { get; }
        public abstract void Remove(ITableIndex begin, ITableEditor editor);
        public abstract void Draw(ITableIndex begin, ITableEditor editor);
        public abstract AssignmentProgress GetValue(ITableIndex begin, ITableDataProvider provider);
        public abstract void SetValue(AssignmentProgress value, ITableIndex begin, ITableEditor editor);
    }
}