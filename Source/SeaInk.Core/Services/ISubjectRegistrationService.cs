using SeaInk.Core.Entities;

namespace SeaInk.Core.Services
{
    public interface ISubjectRegistrationService
    {
        Subject RegisterSubject(int universityId);
    }
}