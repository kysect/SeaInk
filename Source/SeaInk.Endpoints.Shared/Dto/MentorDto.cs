using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public record MentorDto(
        int Id,
        int UniversityId,
        string FirstName,
        string LastName,
        string MiddleName,
        string FullName);

    public static class MentorExtension
    {
        public static MentorDto ToDto(this Mentor mentor)
        {
            return new MentorDto(mentor.Id,
                                 mentor.UniversityId,
                                 mentor.FirstName,
                                 mentor.LastName,
                                 mentor.MiddleName,
                                 mentor.FullName);
        }
    }
}