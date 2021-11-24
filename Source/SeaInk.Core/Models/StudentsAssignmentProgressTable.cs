using System.Collections.Generic;
using SeaInk.Core.Entities;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Models
{
    public class StudentsAssignmentProgressTable
    {
        public StudentsAssignmentProgressTable(
            IReadOnlyCollection<Student> students,
            IReadOnlyCollection<StudyAssignment> assignments,
            IReadOnlyCollection<StudentAssignmentProgress> progresses)
        {
            Students = students.ThrowIfNull(nameof(students));
            Assignments = assignments.ThrowIfNull(nameof(assignments));
            Progresses = progresses.ThrowIfNull(nameof(progresses));
        }

        public IReadOnlyCollection<Student> Students { get; }
        public IReadOnlyCollection<StudyAssignment> Assignments { get; }
        public IReadOnlyCollection<StudentAssignmentProgress> Progresses { get; }
    }
}