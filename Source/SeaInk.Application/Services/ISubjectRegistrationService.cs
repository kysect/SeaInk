using SeaInk.Application.Dtos;

namespace SeaInk.Application.Services
{
    public interface ISubjectRegistrationService
    {
        SubjectDto RegisterSubject(int universityId);
    }
}