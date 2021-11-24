using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public class User : IEquatable<User>
    {
        public User(int universityId, string firstName, string lastName, string middleName)
        {
            UniversityId = universityId;
            FirstName = firstName.ThrowIfNull(nameof(firstName));
            MiddleName = middleName.ThrowIfNull(nameof(middleName));
            LastName = lastName.ThrowIfNull(nameof(lastName));
        }

        [Key]
        public int Id { get; protected init; }

        public int UniversityId { get; protected init; }

        public string FirstName { get; protected init; }
        public string LastName { get; protected init; }
        public string MiddleName { get; protected init; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName} {MiddleName}";

        public bool Equals(User? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as User);

        public override int GetHashCode()
            => Id;
    }
}