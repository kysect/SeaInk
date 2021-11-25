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
            UniversityId = universityId;
            Name = name.ThrowIfNull(nameof(name));
        }

        [Key]
        public int Id { get; private init; }

        public int UniversityId { get; private init; }

        public string Name { get; private init; }

        public IReadOnlyCollection<Student> Students => _students;

        public bool Equals(StudyGroup? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as StudyGroup);

        public override int GetHashCode()
            => Id;

        public void AddStudents(params Student[] students)
        {
            students.ThrowIfNull(nameof(students));

            foreach (Student student in _students)
            {
                student.Group = this;
                _students.Add(student);
            }
        }
    }
}