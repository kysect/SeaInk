using System.Collections.Generic;
using System.Linq;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;
using SeaInk.Core.StudyTable;
using SeaInk.Core.TableLayout.Models;

namespace SeaInk.Core.Extensions
{
    public static class TableModelExtensions
    {
        public static StudentsAssignmentProgressTable ToStudentsAssignmentProgressTable(
            this TableModel model, StudyStudentGroup studyStudentGroup, Subject subject)
        {
            StudentGroup group = studyStudentGroup.StudentGroup;
            IReadOnlyCollection<Assignment> assignments = subject.Assignments;

            var progresses = model.Rows
                .Select(r => (Student: FindStudent(group, r.Student), r.AssignmentProgresses))
                .SelectMany(
                    r => r.AssignmentProgresses,
                    (r, p)
                        => new StudentAssignmentProgress(r.Student, FindAssignment(p.Assignment, assignments), p.Progress))
                .ToList();

            return new StudentsAssignmentProgressTable(group.Students, assignments, progresses);
        }

        private static Student FindStudent(StudentGroup group, StudentModel model)
            => group.Students.Single(s => s.FullName.Equals(model.Name));

        private static Assignment FindAssignment(AssignmentModel model, IReadOnlyCollection<Assignment> assignments)
            => assignments.Single(a => a.Title.Equals(model.Title));
    }
}