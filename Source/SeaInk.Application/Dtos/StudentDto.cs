using System;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Dtos
{
    public class StudentDto : UserDto, IEquatable<StudentDto>
    {
        public StudentDto(Guid id, int universityId, string firstName, string middleName, string lastName, StudyGroupDto studyGroup)
            : base(id, universityId, firstName, middleName, lastName)
        {
            StudyGroup = studyGroup.ThrowIfNull();
        }

        public StudyGroupDto StudyGroup { get; }

        public bool Equals(StudentDto? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as StudentDto);

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}