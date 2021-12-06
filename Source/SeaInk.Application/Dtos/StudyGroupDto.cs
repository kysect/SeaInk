using System;
using System.Collections.Generic;
using System.Linq;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Dtos
{
    public sealed class StudyGroupDto : IEquatable<StudyGroupDto>
    {
        public StudyGroupDto(Guid id, int universityId, string name, IReadOnlyCollection<StudentDto> students)
        {
            Id = id;
            UniversityId = universityId;
            Name = name.ThrowIfNull();
            Students = students.ThrowIfNull().ToList();
        }

        public Guid Id { get; }
        public int UniversityId { get; }
        public string Name { get; }
        public IReadOnlyCollection<StudentDto> Students { get; }

        public bool Equals(StudyGroupDto? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as StudyGroupDto);

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}