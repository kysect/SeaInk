using SeaInk.Core.Entities;

namespace SeaInk.Application.Services
{
    public interface ISubjectRegistrationService
    {
        Subject RegisterSubject(int universityId);
    }
}