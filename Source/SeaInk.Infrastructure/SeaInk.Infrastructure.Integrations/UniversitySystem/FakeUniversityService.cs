using Bogus;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;
using SeaInk.Core.Services;
using SeaInk.Core.StudyTable;
using SeaInk.Core.UniversityServiceModels;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.Integrations.UniversitySystem
{
    public class FakeUniversityService : IUniversityService
    {
        private static readonly Faker<SubjectUniversityModel> SubjectFaker = new Faker<SubjectUniversityModel>()
            .CustomInstantiator(f => new SubjectUniversityModel(f.IndexFaker, f.Name.JobArea()));

        private static readonly Faker<StudentUniversityModel> StudentFaker = new Faker<StudentUniversityModel>()
            .CustomInstantiator(f => new StudentUniversityModel(
                                    f.IndexFaker,
                                    f.Person.FirstName,
                                    f.Person.LastName,
                                    f.Person.UserName));

        private static readonly Faker<StudentGroupUniversityModel> StudyGroupFaker = new Faker<StudentGroupUniversityModel>()
            .CustomInstantiator(f => new StudentGroupUniversityModel(f.IndexFaker, $"{f.Lorem.Letter().ToUpper()}{f.Random.Int(1000, 9999)}"));

        private static readonly Faker<AssignmentUniversityModel> AssignmentFaker = new Faker<AssignmentUniversityModel>()
            .CustomInstantiator(f => new AssignmentUniversityModel(
                                    f.IndexFaker,
                                    f.Name.JobDescriptor(),
                                    f.Random.Bool(0.2f),
                                    f.Date.Past(),
                                    f.Date.Future(),
                                    f.Random.Double(0, 5),
                                    f.Random.Double(5, 10)));

        public Task<Mentor> GetMentorAsync(int universityId, CancellationToken cancellationToken)
        {
            Faker<Mentor> faker = new Faker<Mentor>()
                .CustomInstantiator(f => new Mentor(universityId, f.Person.FirstName, f.Person.LastName, f.Person.UserName));

            return Task.FromResult(faker.Generate());
        }

        public Task<IReadOnlyCollection<SubjectUniversityModel>> GetMentorSubjectsAsync(Mentor mentor, CancellationToken cancellationToken)
        {
            mentor.ThrowIfNull();
            return Task.FromResult((IReadOnlyCollection<SubjectUniversityModel>)SubjectFaker.Generate(6));
        }

        public Task<IReadOnlyCollection<StudentGroupUniversityModel>> GetMentorSubjectGroupsAsync(
            Mentor mentor, Subject subject, CancellationToken cancellationToken)
        {
            mentor.ThrowIfNull();
            subject.ThrowIfNull();

            return Task.FromResult((IReadOnlyCollection<StudentGroupUniversityModel>)StudyGroupFaker.Generate(3));
        }

        public Task<IReadOnlyCollection<AssignmentUniversityModel>> GetSubjectAssignmentsAsync(Subject subject, CancellationToken cancellationToken)
        {
            subject.ThrowIfNull();

            return Task.FromResult((IReadOnlyCollection<AssignmentUniversityModel>)AssignmentFaker.Generate(8));
        }

        public Task<IReadOnlyCollection<StudentUniversityModel>> GetGroupStudentsAsync(StudentGroup group, CancellationToken cancellationToken)
        {
            group.ThrowIfNull();

            return Task.FromResult((IReadOnlyCollection<StudentUniversityModel>)StudentFaker.Generate(20));
        }

        public Task<StudentsAssignmentProgressTable> GetStudentAssignmentProgressTableAsync(
            StudyStudentGroup studyStudentGroup, CancellationToken cancellationToken)
        {
            studyStudentGroup.ThrowIfNull();

            Faker<StudentAssignmentProgress> faker = new Faker<StudentAssignmentProgress>()
                .CustomInstantiator(f => new StudentAssignmentProgress(
                                        f.Random.ArrayElement(studyStudentGroup.StudentGroup.Students.ToArray()),
                                        AssignmentFaker.Generate().ToAssignment(),
                                        new AssignmentProgress(f.Random.Double())));

            var table = new StudentsAssignmentProgressTable(
                studyStudentGroup.StudentGroup.Students,
                AssignmentFaker.Generate(10).Select(AssignmentUniversityModelExtensions.ToAssignment).ToList(),
                faker.Generate(20));

            return Task.FromResult(table);
        }

        public Task SetStudentAssignmentProgressesAsync(
            IReadOnlyCollection<StudentAssignmentProgress> progresses, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}