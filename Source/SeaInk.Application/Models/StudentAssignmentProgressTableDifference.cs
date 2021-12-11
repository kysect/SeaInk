using System.Collections.Generic;
using System.Linq;
using SeaInk.Application.Exceptions;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Models
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

        public StudentAssignmentProgressTableDifference(
            IReadOnlyCollection<Student> removedStudents,
            IReadOnlyCollection<Student> addedStudents,
            IReadOnlyCollection<StudyAssignment> removedAssignments,
            IReadOnlyCollection<StudyAssignment> addedAssignments,
            IReadOnlyCollection<StudentAssignmentProgressDifference> assignmentProgressDifferences)
        {
            removedStudents.ThrowIfNull();
            addedStudents.ThrowIfNull();
            removedAssignments.ThrowIfNull();
            addedAssignments.ThrowIfNull();
            assignmentProgressDifferences.ThrowIfNull();

            if (removedStudents.Intersect(addedStudents).Any())
                throw new AddRemoveDifferenceIntersectException<Student>();

            if (removedAssignments.Intersect(addedAssignments).Any())
                throw new AddRemoveDifferenceIntersectException<StudyAssignment>();

            RemovedStudents = removedStudents;
            AddedStudents = addedStudents;

            RemovedAssignments = removedAssignments;
            AddedAssignments = addedAssignments;

            AssignmentProgressDifferences = assignmentProgressDifferences;
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