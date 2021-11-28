using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.Components
{
    public class StudentsColumnComponent : LayoutComponent,
                                           IValueGettingLayoutComponent<StudentModel>,
                                           IValueSettingLayoutComponent<StudentModel>
    {
        public override Frame Frame => new Frame(1, 1);

        public override bool Equals(LayoutComponent? other)
            => other is StudentsColumnComponent;

        public StudentModel GetValue(ITableIndex begin, ITableDataProvider provider)
            => new StudentModel(provider[begin]);

        public void SetValue(StudentModel value, ITableIndex begin, ITableEditor editor)
            => editor.EnqueueWrite(begin, new[] { new[] { value.Name } });

        public override int GetHashCode()
            => 0;

        public override bool Equals(object? obj)
            => Equals(obj as LayoutComponent);
    }
}