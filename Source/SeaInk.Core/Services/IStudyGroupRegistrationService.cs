using SeaInk.Core.Entities;

namespace SeaInk.Core.Services
{
    public interface IStudyGroupRegistrationService
    {
        StudyGroup RegisterStudyGroup(int universityId);
    }
}