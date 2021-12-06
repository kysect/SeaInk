using System;

namespace SeaInk.Application.Dtos
{
    public sealed class MentorDto : UserDto, IEquatable<MentorDto>
    {
        public MentorDto(Guid id, int universityId, string firstName, string middleName, string lastName)
            : base(id, universityId, firstName, middleName, lastName) { }

        public bool Equals(MentorDto? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as MentorDto);

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}