using System.Collections.Generic;
using System.Linq;
using SeaInk.Application.Models;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;

namespace SeaInk.Application.Extensions
{
    public static class TableModelExtensions
    {
        public static StudentsAssignmentProgressTable ToStudentsAssignmentProgressTable(this TableModel model, StudyGroupSubject studyGroupSubject)
        {
            StudyGroup group = studyGroupSubject.StudyGroup;
            IReadOnlyCollection<StudyAssignment> assignments = studyGroupSubject.Subject.Assignments;

            var progresses = model.Rows
                .Select(r => (Student: FindStudent(group, r.Student), r.AssignmentProgresses))
                .SelectMany(
                    r => r.AssignmentProgresses,
                    (r, p)
                        => new StudentAssignmentProgress(r.Student, FindAssignment(p.Assignment, assignments), p.Progress))
                .ToList();

            return new StudentsAssignmentProgressTable(group.Students, assignments, progresses);
        }

        private static Student FindStudent(StudyGroup group, StudentModel model)
            => group.Students.Single(s => s.FullName.Equals(model.Name));

        private static StudyAssignment FindAssignment(AssignmentModel model, IReadOnlyCollection<StudyAssignment> assignments)
            => assignments.Single(a => a.Title.Equals(model.Title));
    }
}