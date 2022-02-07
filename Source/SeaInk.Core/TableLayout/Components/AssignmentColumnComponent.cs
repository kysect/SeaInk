using Kysect.Centum.Sheets.Indices;
using Newtonsoft.Json;
using SeaInk.Core.Models;
using SeaInk.Core.TableLayout.CommandInterfaces;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Core.TableLayout.Models;

namespace SeaInk.Core.TableLayout.Components
{
    public abstract class AssignmentColumnComponent : LayoutComponent,
                                                      IDrawableLayoutComponent,
                                                      IRemovableComponent,
                                                      IValueRepresentingLayoutComponent<AssignmentModel>,
                                                      IValueGettingLayoutComponent<AssignmentProgress>,
                                                      IValueSettingLayoutComponent<AssignmentProgress>
    {
        public abstract AssignmentModel Value { get; }
        public abstract void Remove(ISheetIndex begin, ISheetEditor editor);
        public abstract void Draw(ISheetIndex begin, ISheetEditor editor);
        public abstract AssignmentProgress GetValue(ISheetIndex begin, ISheetDataProvider provider);
        public abstract void SetValue(AssignmentProgress value, ISheetIndex begin, ISheetEditor editor);
    }
}