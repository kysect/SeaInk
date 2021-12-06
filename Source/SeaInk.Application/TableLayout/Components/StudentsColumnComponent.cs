using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.Components
{
    public class StudentsColumnComponent : LayoutComponent,
                                           IValueGettingLayoutComponent<StudentModel>,
                                           IValueSettingLayoutComponent<StudentModel>
    {
        public override Frame Frame => new Frame(1, 1);

        public StudentModel GetValue(ISheetIndex begin, ITableDataProvider provider)
            => new StudentModel(provider[begin]);

        public void SetValue(StudentModel value, ISheetIndex begin, ITableEditor editor)
            => editor.EnqueueWrite(begin, new[] { new[] { value.Name } });

        public override bool Equals(LayoutComponent? other)
            => other is StudentsColumnComponent;

        public override bool Equals(object? obj)
            => Equals(obj as LayoutComponent);

        public override int GetHashCode()
            => 0;
    }
}