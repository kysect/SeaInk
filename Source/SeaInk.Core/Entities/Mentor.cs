using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public sealed class Mentor : IEquatable<Mentor>
    {
        public Mentor(int universityId, string firstName, string lastName, string middleName)
        {
            Id = Guid.NewGuid();
            UniversityId = universityId;
            FirstName = firstName.ThrowIfNull();
            MiddleName = middleName.ThrowIfNull();
            LastName = lastName.ThrowIfNull();
        }

        [Key]
        public Guid Id { get; private init; }

        public int UniversityId { get; private init; }

        public string FirstName { get; private init; }
        public string LastName { get; private init; }
        public string MiddleName { get; private init; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName} {MiddleName}";

        public bool Equals(Mentor? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as Mentor);

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}