using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public sealed class Subject : IEquatable<Subject>
    {
        private readonly List<StudyAssignment> _assignments = new List<StudyAssignment>();

        public Subject(int universityId, string name)
        {
            Id = Guid.NewGuid();
            UniversityId = universityId;
            Name = name.ThrowIfNull();
        }

        [Key]
        public Guid Id { get; private init; }

        public int UniversityId { get; private init; }

        public string Name { get; private init; }

        public IReadOnlyCollection<StudyAssignment> Assignments => _assignments;

        public bool Equals(Subject? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as Subject);

        public override int GetHashCode()
            => Id.GetHashCode();

        public void AddAssignments(params StudyAssignment[] assignments)
        {
            assignments.ThrowIfNull();
            _assignments.AddRange(assignments);
        }
    }
}