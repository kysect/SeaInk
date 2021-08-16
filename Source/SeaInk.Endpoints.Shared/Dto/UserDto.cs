using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public record UserDto(
        int Id,
        int UniversityId,
        string FirstName,
        string LastName,
        string MiddleName,
        string FullName);

    public static class UserExtension
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto(user.Id, user.UniversityId, user.FirstName, 
                               user.LastName, user.MiddleName, user.FullName);
        }
    }
}