using SeaInk.Application.Dtos;

namespace SeaInk.Application.Services
{
    public interface IStudyGroupRegistrationService
    {
        StudyGroupDto RegisterStudyGroup(int universityId);
    }
}