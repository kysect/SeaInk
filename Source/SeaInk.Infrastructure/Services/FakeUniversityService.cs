using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using SeaInk.Application.Models;
using SeaInk.Application.Services;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.Services
{
    public class FakeUniversityService : IUniversityService
    {
        public Task<Mentor> GetMentorAsync(int universityId)
        {
            Faker<Mentor> faker = new Faker<Mentor>()
                .CustomInstantiator(f => new Mentor(universityId, f.Person.FirstName, f.Person.LastName, f.Person.UserName));

            return Task.FromResult(faker.Generate());
        }

        public Task<IReadOnlyCollection<SubjectModel>> GetMentorSubjectsAsync(Mentor mentor)
        {
            mentor.ThrowIfNull();

            Faker<SubjectModel> faker = new Faker<SubjectModel>()
                .CustomInstantiator(f => new SubjectModel(f.IndexFaker, f.Name.JobArea()));

            return Task.FromResult((IReadOnlyCollection<SubjectModel>)faker.Generate(6));
        }

        public Task<IReadOnlyCollection<StudyGroupModel>> GetMentorSubjectGroupsAsync(Mentor mentor, Subject subject)
        {
            mentor.ThrowIfNull();
            subject.ThrowIfNull();

            Faker<StudyGroupModel> faker = new Faker<StudyGroupModel>()
                .CustomInstantiator(f => new StudyGroupModel(f.IndexFaker, $"{f.Lorem.Letter().ToUpper()}{f.Random.Int(1000, 9999)}"));

            return Task.FromResult((IReadOnlyCollection<StudyGroupModel>)faker.Generate(3));
        }

        public Task<Subject> GetSubjectAsync(SubjectModel subjectModel)
        {
            subjectModel.ThrowIfNull();
            return Task.FromResult(new Subject(subjectModel.UniversityId, subjectModel.Title));
        }

        public Task<Subject> UpdateSubjectAsync(Subject subject)
        {
            subject.ThrowIfNull();
            return Task.FromResult(subject);
        }

        public Task<StudyGroup> GetGroupAsync(StudyGroupModel groupModel)
        {
            groupModel.ThrowIfNull();
            return Task.FromResult(new StudyGroup(groupModel.UniversityId, groupModel.Name));
        }

        public Task<StudyGroup> UpdateGroupAsync(StudyGroup group)
        {
            group.ThrowIfNull();
            return Task.FromResult(group);
        }

        public Task<StudentsAssignmentProgressTable> GetStudentAssignmentProgressTableAsync(StudyGroupSubject studyGroupSubject)
        {
            studyGroupSubject.ThrowIfNull();

            Faker<StudentAssignmentProgress> faker = new Faker<StudentAssignmentProgress>()
                .CustomInstantiator(f => new StudentAssignmentProgress(
                                        f.Random.ArrayElement(studyGroupSubject.StudyGroup.Students.ToArray()),
                                        f.Random.ArrayElement(studyGroupSubject.Subject.Assignments.ToArray()),
                                        new AssignmentProgress(f.Random.Double())));

            var table = new StudentsAssignmentProgressTable(
                studyGroupSubject.StudyGroup.Students, studyGroupSubject.Subject.Assignments, faker.Generate(20));

            return Task.FromResult(table);
        }

        public Task SetStudentAssignmentProgressesAsync(IReadOnlyCollection<StudentAssignmentProgress> progresses)
            => Task.CompletedTask;
    }
}