using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public sealed class Subject : IEquatable<Subject>
    {
        private readonly List<StudyAssignment> _assignments = new List<StudyAssignment>();

        public Subject(int universityId, string name, DateTime startDate, DateTime endDate)
        {
            UniversityId = universityId;
            Name = name.ThrowIfNull(nameof(name));
            StartDate = startDate;
            EndDate = endDate;
        }

        [Key]
        public int Id { get; private init; }

        public int UniversityId { get; private init; }

        public string Name { get; private init; }

        public DateTime StartDate { get; private init; }
        public DateTime EndDate { get; private init; }

        public IReadOnlyCollection<StudyAssignment> Assignments => _assignments;

        public bool Equals(Subject? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as Subject);

        public override int GetHashCode()
            => Id;

        internal void AddAssignments(params StudyAssignment[] assignments)
        {
            assignments.ThrowIfNull(nameof(assignments));
            _assignments.AddRange(assignments);
        }
    }
}