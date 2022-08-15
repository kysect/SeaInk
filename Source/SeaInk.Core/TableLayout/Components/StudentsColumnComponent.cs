using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.CommandInterfaces;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Core.TableLayout.Models;

namespace SeaInk.Core.TableLayout.Components
{
    public class StudentsColumnComponent : LayoutComponent,
                                           IValueGettingLayoutComponent<StudentModel>,
                                           IValueSettingLayoutComponent<StudentModel>
    {
        public override Frame Frame => new Frame(1, 1);

        public StudentModel GetValue(ISheetIndex begin, ISheetDataProvider provider)
            => new StudentModel(provider[begin]);

        public void SetValue(StudentModel value, ISheetIndex begin, ISheetEditor editor)
            => editor.EnqueueWrite(begin, new[] { new[] { value.Name } });

        public override bool Equals(LayoutComponent? other)
            => other is StudentsColumnComponent;

        public override bool Equals(object? obj)
            => Equals(obj as LayoutComponent);

        public override int GetHashCode()
            => 0;
    }
}