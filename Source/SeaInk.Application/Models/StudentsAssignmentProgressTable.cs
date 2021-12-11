using System.Collections.Generic;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Models
{
    public class StudentsAssignmentProgressTable
    {
        public StudentsAssignmentProgressTable(
            IReadOnlyCollection<Student> students,
            IReadOnlyCollection<StudyAssignment> assignments,
            IReadOnlyCollection<StudentAssignmentProgress> progresses)
        {
            Students = students.ThrowIfNull();
            Assignments = assignments.ThrowIfNull();
            Progresses = progresses.ThrowIfNull();
        }

        public IReadOnlyCollection<Student> Students { get; }
        public IReadOnlyCollection<StudyAssignment> Assignments { get; }
        public IReadOnlyCollection<StudentAssignmentProgress> Progresses { get; }
    }
}