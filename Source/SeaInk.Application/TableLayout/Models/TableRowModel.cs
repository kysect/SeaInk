using System.Collections.Generic;
using System.Linq;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Models
{
    public class TableRowModel
    {
        public TableRowModel(StudentModel student, IReadOnlyCollection<AssignmentProgressModel> assignmentProgresses)
        {
            Student = student.ThrowIfNull(nameof(student));
            AssignmentProgresses = assignmentProgresses.ThrowIfNull(nameof(student)).ToList();
        }

        public StudentModel Student { get; set; }
        public ICollection<AssignmentProgressModel> AssignmentProgresses { get; }
    }
}