using SeaInk.Core.Entities;

namespace SeaInk.Application.Services
{
    public interface IStudyGroupRegistrationService
    {
        StudyGroup RegisterStudyGroup(int universityId);
    }
}