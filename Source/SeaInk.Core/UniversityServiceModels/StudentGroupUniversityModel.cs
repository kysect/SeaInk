using SeaInk.Core.Entities;

namespace SeaInk.Core.UniversityServiceModels
{
    public record StudentGroupUniversityModel(int UniversityId, string Name);

    public static class StudentGroupUniversityModelExtensions
    {
        public static StudentGroup ToStudentGroup(this StudentGroupUniversityModel model)
            => new StudentGroup(model.UniversityId, model.Name);
    }
}