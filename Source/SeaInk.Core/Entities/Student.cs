using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public class Student : IEqualityComparer<Student>
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
        protected Student() { }
#pragma warning restore CS8618

        [Key]
        public Guid Id { get; private init; }

        public int UniversityId { get; private init; }

        public string FirstName { get; private init; }
        public string LastName { get; private init; }
        public string MiddleName { get; private init; }

        public string FullName => $"{FirstName} {LastName} {MiddleName}";

        public bool Equals(Student? x, Student? y)
            => x is not null && y is not null && x.Id.Equals(y.Id);

        public int GetHashCode(Student obj)
            => obj.Id.GetHashCode();
    }
}