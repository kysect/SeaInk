using System;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Application.TableLayout.Visitors;

namespace SeaInk.Application.TableLayout.Components
{
    public class StudentsColumnComponent : HeaderLayoutComponent<ITableRowVisitor>
    {
        public StudentsColumnComponent()
            : base(Array.Empty<LabelComponent>()) { }

        public override void SetVisit(ITableRowVisitor value, ITableIndex begin, ITableEditor editor)
            => editor.EnqueueWrite(begin, new[] { new[] { value.GetStudent().Name } });

        public override void GetVisit(ITableRowVisitor value, ITableIndex begin, ITableDataProvider provider)
        {
            string name = provider[begin];
            value.SetStudent(new StudentModel(name));
        }
    }
}