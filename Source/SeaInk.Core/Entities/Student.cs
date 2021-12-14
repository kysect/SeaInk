using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public sealed class Student : IEquatable<Student>
    {
        public Student(int universityId, string firstName, string lastName, string middleName)
        {
            Id = Guid.NewGuid();
            UniversityId = universityId;
            FirstName = firstName.ThrowIfNull();
            LastName = lastName.ThrowIfNull();
            MiddleName = middleName.ThrowIfNull();
        }

#pragma warning disable CS8618
        private Student() { }
#pragma warning restore CS8618

        [Key]
        public Guid Id { get; private init; }

        public int UniversityId { get; private init; }

        public string FirstName { get; private init; }
        public string LastName { get; private init; }
        public string MiddleName { get; private init; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName} {MiddleName}";

        public bool Equals(Student? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as Student);

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}