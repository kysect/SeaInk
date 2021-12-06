using System.Collections.Generic;
using System.Linq;
using SeaInk.Core.Entities;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Models
{
    public class StudentAssignmentProgressTableDifference
    {
        public StudentAssignmentProgressTableDifference(StudentsAssignmentProgressTable left, StudentsAssignmentProgressTable right)
        {
            left.ThrowIfNull();
            right.ThrowIfNull();

            RemovedStudents = left.Students.Except(right.Students).ToList();
            AddedStudents = right.Students.Except(left.Students).ToList();

            RemovedAssignments = left.Assignments.Except(right.Assignments).ToList();
            AddedAssignments = right.Assignments.Except(left.Assignments).ToList();

            AssignmentProgressDifferences = CalculateAssignmentProgressDifferences(left.Progresses, right.Progresses);
        }

        public IReadOnlyCollection<Student> RemovedStudents { get; }
        public IReadOnlyCollection<Student> AddedStudents { get; }

        public IReadOnlyCollection<StudyAssignment> RemovedAssignments { get; }
        public IReadOnlyCollection<StudyAssignment> AddedAssignments { get; }

        public IReadOnlyCollection<StudentAssignmentProgressDifference> AssignmentProgressDifferences { get; }

        private static IReadOnlyCollection<StudentAssignmentProgressDifference> CalculateAssignmentProgressDifferences(
            IReadOnlyCollection<StudentAssignmentProgress> left, IReadOnlyCollection<StudentAssignmentProgress> right)
        {
            bool AssignmentComparer(StudentAssignmentProgress p, StudentAssignmentProgress op)
                => p.Student.Equals(op.Student) && p.Assignment.Equals(op.Assignment);

            IEnumerable<StudentAssignmentProgress> addedProgresses = right
                .Where(op => !left.Any(p => AssignmentComparer(p, op)));
            var addedProgressesDiff = addedProgresses
                .Select(p => new StudentAssignmentProgressDifference(
                            p.Student, p.Assignment, null, p.Progress))
                .ToList();

            var modifiedProgresses = new List<StudentAssignmentProgressDifference>();

            foreach (StudentAssignmentProgress progress in left)
            {
                StudentAssignmentProgress? otherProgress = right
                    .SingleOrDefault(op => AssignmentComparer(progress, op));

                var diff = new StudentAssignmentProgressDifference(
                    progress.Student, progress.Assignment, progress.Progress, otherProgress?.Progress);

                modifiedProgresses.Add(diff);
            }

            return modifiedProgresses.Concat(addedProgressesDiff).ToList();
        }
    }
}