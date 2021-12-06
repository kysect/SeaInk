using SeaInk.Application.Dtos;

namespace SeaInk.Application.Services
{
    public interface IMentorRegistrationService
    {
        MentorDto RegisterMentor(int universityId);
    }
}