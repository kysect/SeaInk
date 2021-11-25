using SeaInk.Core.Entities;

namespace SeaInk.Application.Services
{
    public interface IMentorRegistrationService
    {
        Mentor RegisterMentor(int universityId);
    }
}