using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.ComponentsBase;
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
        public abstract void Remove(ISheetIndex begin, ITableEditor editor);
        public abstract void Draw(ISheetIndex begin, ITableEditor editor);
        public abstract AssignmentProgress GetValue(ISheetIndex begin, ITableDataProvider provider);
        public abstract void SetValue(AssignmentProgress value, ISheetIndex begin, ITableEditor editor);
    }
}