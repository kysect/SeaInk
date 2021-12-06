using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public sealed class StudyGroup : IEquatable<StudyGroup>
    {
        private readonly List<Student> _students = new List<Student>();

        public StudyGroup(int universityId, string name)
        {
            Id = Guid.NewGuid();
            UniversityId = universityId;
            Name = name.ThrowIfNull();
        }

        [Key]
        public Guid Id { get; private init; }

        public int UniversityId { get; private init; }

        public string Name { get; private init; }

        public IReadOnlyCollection<Student> Students => _students;

        public bool Equals(StudyGroup? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as StudyGroup);

        public override int GetHashCode()
            => Id.GetHashCode();

        public void AddStudents(params Student[] students)
        {
            students.ThrowIfNull();

            foreach (Student student in _students)
            {
                student.Group = this;
                _students.Add(student);
            }
        }
    }
}