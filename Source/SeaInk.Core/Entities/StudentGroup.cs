using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SeaInk.Core.Entities.Exceptions;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public sealed class StudentGroup : IEquatable<StudentGroup>
    {
        private readonly List<Student> _students = new List<Student>();

        public StudentGroup(int universityId, string name)
        {
            Id = Guid.NewGuid();
            UniversityId = universityId;
            Name = name.ThrowIfNull();
        }

#pragma warning disable CS8618
        private StudentGroup() { }
#pragma warning restore CS8618

        [Key]
        public Guid Id { get; private init; }

        public int UniversityId { get; private init; }

        public string Name { get; private init; }

        public IReadOnlyCollection<Student> Students => _students;

        public void AddStudents(params Student[] students)
        {
            students.ThrowIfNull();

            if (students.Any(s => _students.Contains(s)))
                throw new ContainingStudentsException(this);

            _students.AddRange(students);
        }

        public void RemoveStudents(params Student[] students)
        {
            students.ThrowIfNull();

            if (students.Any(s => !_students.Contains(s)))
                throw new NotContainingStudentsException(this);

            foreach (Student student in students)
            {
                _students.Remove(student);
            }
        }

        public bool Equals(StudentGroup? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as StudentGroup);

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}