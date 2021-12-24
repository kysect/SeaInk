using SeaInk.Core.Entities;

namespace SeaInk.Core.UniversityServiceModels
{
    public record SubjectUniversityModel(int UniversityId, string Title);

    public static class SubjectUniversityModelExtensions
    {
        public static Subject ToSubject(this SubjectUniversityModel model)
            => new Subject(model.UniversityId, model.Title);
    }
}