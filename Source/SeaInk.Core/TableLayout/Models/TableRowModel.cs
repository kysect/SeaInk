using System.Collections.Generic;
using System.Linq;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.TableLayout.Models
{
    public class TableRowModel
    {
        public TableRowModel(StudentModel student, IReadOnlyCollection<AssignmentProgressModel> assignmentProgresses)
        {
            Student = student.ThrowIfNull();
            AssignmentProgresses = assignmentProgresses.ThrowIfNull().ToList();
        }

        public StudentModel Student { get; set; }
        public IReadOnlyCollection<AssignmentProgressModel> AssignmentProgresses { get; }
    }
}