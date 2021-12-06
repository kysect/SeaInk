using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public class User : IEqualityComparer<User>
    {
        public User(int universityId, string firstName, string lastName, string middleName)
        {
            Id = Guid.NewGuid();
            UniversityId = universityId;
            FirstName = firstName.ThrowIfNull();
            MiddleName = middleName.ThrowIfNull();
            LastName = lastName.ThrowIfNull();
        }

        [Key]
        public Guid Id { get; protected init; }
        public int UniversityId { get; protected init; }

        public string FirstName { get; protected init; }
        public string LastName { get; protected init; }
        public string MiddleName { get; protected init; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName} {MiddleName}";

        public bool Equals(User? x, User? y)
            => x is not null &&
               y is not null &&
               x.Id.Equals(y.Id);

        public int GetHashCode(User obj)
            => obj.Id.GetHashCode();
    }
}