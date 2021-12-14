using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SeaInk.Core.Entities.Exceptions;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public sealed class Subject : IEquatable<Subject>
    {
        private readonly List<Assignment> _assignments = new List<Assignment>();

        public Subject(int universityId, string name)
        {
            Id = Guid.NewGuid();
            UniversityId = universityId;
            Name = name.ThrowIfNull();
        }

#pragma warning disable CS8618
        private Subject() { }
#pragma warning restore CS8618

        [Key]
        public Guid Id { get; private init; }

        public int UniversityId { get; private init; }
        public string Name { get; private init; }
        public IReadOnlyCollection<Assignment> Assignments => _assignments;

        public void AddAssignments(IReadOnlyCollection<Assignment> assignments)
        {
            assignments.ThrowIfNull();

            if (assignments.Any(a => _assignments.Contains(a)))
                throw new ContainingAssignmentsException(this);

            _assignments.AddRange(assignments);
        }

        public void RemoveAssignments(IReadOnlyCollection<Assignment> assignments)
        {
            assignments.ThrowIfNull();

            if (assignments.Any(a => !_assignments.Contains(a)))
                throw new NotContainingAssignmentsException(this);

            foreach (Assignment assignment in assignments)
            {
                _assignments.Remove(assignment);
            }
        }

        public bool Equals(Subject? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as Subject);

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}