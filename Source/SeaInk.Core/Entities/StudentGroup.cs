using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SeaInk.Core.Entities.Exceptions;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public class StudentGroup : IEqualityComparer<StudentGroup>
    {
        private readonly List<Student> _students = new List<Student>();

        public StudentGroup(int universityId, string name)
        {
            Id = Guid.NewGuid();
            UniversityId = universityId;
            Name = name.ThrowIfNull();
        }

#pragma warning disable CS8618
        protected StudentGroup() { }
#pragma warning restore CS8618

        [Key]
        public Guid Id { get; private init; }

        public int UniversityId { get; private init; }

        public string Name { get; private init; }

        public virtual IReadOnlyCollection<Student> Students => _students;

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

        public bool Equals(StudentGroup? x, StudentGroup? y)
            => x is not null && y is not null && x.Id.Equals(y.Id);

        public int GetHashCode(StudentGroup obj)
            => obj.Id.GetHashCode();
    }
}