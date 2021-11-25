using System.Collections.Generic;
using Bogus;
using SeaInk.Application.Services;
using SeaInk.Application.UniversityEntityModels;

namespace SeaInk.Infrastructure.Services
{
    public class FakeUniversitySystemService : IUniversitySystemService
    {
        private readonly Faker<UniversityUserModel> _userFaker;
        private readonly Faker<UniversityStudyGroupModel> _groupFaker;
        private readonly Faker<UniversitySubjectModel> _subjectFaker;
        private readonly Faker<UniversityStudyAssignmentModel> _assignmentFaker;

        public FakeUniversitySystemService()
        {
            _userFaker = new Faker<UniversityUserModel>("ru")
                .CustomInstantiator(
                    f => new UniversityUserModel(
                        f.IndexFaker,
                        f.Person.FirstName,
                        f.Person.UserName,
                        f.Person.LastName));

            _groupFaker = new Faker<UniversityStudyGroupModel>()
                .CustomInstantiator(
                    f => new UniversityStudyGroupModel(
                        f.IndexFaker,
                        $"{f.Lorem.Letter()}{f.Random.Number(1000, 9999)}"));

            _subjectFaker = new Faker<UniversitySubjectModel>()
                .CustomInstantiator(
                    f => new UniversitySubjectModel(
                        f.IndexFaker,
                        f.Date.Past(),
                        f.Date.Future()));

            _assignmentFaker = new Faker<UniversityStudyAssignmentModel>()
                .CustomInstantiator(
                    f => new UniversityStudyAssignmentModel(
                        f.IndexFaker,
                        f.Name.JobType(),
                        f.Random.Bool(0.2f),
                        f.Date.Past(),
                        f.Date.Future(),
                        f.Random.Double(0, 5),
                        f.Random.Double(5, 10)));
        }

        public UniversityUserModel GetMentor(int mentorUniversityId)
        {
            Faker<UniversityUserModel> faker = new Faker<UniversityUserModel>("ru")
                .CustomInstantiator(
                    f => new UniversityUserModel(
                        mentorUniversityId,
                        f.Person.FirstName,
                        f.Person.UserName,
                        f.Person.LastName));

            return faker.Generate();
        }

        public IReadOnlyCollection<UniversitySubjectModel> GetMentorSubjects(int mentorUniversityId)
            => _subjectFaker.Generate(10);

        public IReadOnlyCollection<UniversityStudyGroupModel> GetMentorSubjectGroups(int mentorUniversityId, int subjectUniversityId)
            => _groupFaker.Generate(2);

        public IReadOnlyCollection<UniversityUserModel> GetGroupStudents(int groupUniversityId)
            => _userFaker.Generate(20);

        public IReadOnlyCollection<UniversityStudyAssignmentModel> GetSubjectAssignments(int subjectUniversityId)
            => _assignmentFaker.Generate(8);
    }
}