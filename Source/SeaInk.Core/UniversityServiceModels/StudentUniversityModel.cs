using SeaInk.Core.Entities;

namespace SeaInk.Core.UniversityServiceModels
{
    public record StudentUniversityModel(int UniversityId, string FirstName, string LastName, string MiddleName);

    public static class StudentUniversityModelExtensions
    {
        public static Student ToStudent(this StudentUniversityModel model)
            => new Student(model.UniversityId, model.FirstName, model.LastName, model.MiddleName);
    }
}