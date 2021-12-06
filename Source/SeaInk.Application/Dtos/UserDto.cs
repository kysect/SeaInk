using System;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Dtos
{
    public class UserDto
    {
        public UserDto(Guid id, int universityId, string firstName, string middleName, string lastName)
        {
            Id = id;
            UniversityId = universityId;
            FirstName = firstName.ThrowIfNull();
            MiddleName = middleName.ThrowIfNull();
            LastName = lastName.ThrowIfNull();
        }

        public Guid Id { get; }
        public int UniversityId { get; }

        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }

        public string FullName => $"{FirstName} {LastName} {MiddleName}";
    }
}