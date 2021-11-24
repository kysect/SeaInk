using SeaInk.Core.Entities;

namespace SeaInk.Core.Services
{
    public interface IMentorRegistrationService
    {
        Mentor RegisterMentor(int universityId);
    }
}