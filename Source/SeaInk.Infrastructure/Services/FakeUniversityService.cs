using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using SeaInk.Application.Models;
using SeaInk.Application.Services;
using SeaInk.Core.Entities;
using SeaInk.Infrastructure.Database;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.Services
{
    public class FakeUniversityService : IUniversityService
    {
        private readonly DatabaseContext _context;

        public FakeUniversityService(DatabaseContext context)
        {
            _context = context.ThrowIfNull();
        }

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

            Subject? subject = _context.Subjects.AsEnumerable().SingleOrDefault(s => s.UniversityId.Equals(subjectModel.UniversityId));

            if (subject is null)
            {
                subject = new Subject(subjectModel.UniversityId, subjectModel.Title);
                _context.Subjects.Add(subject);
                _context.SaveChanges();
            }

            return Task.FromResult(subject);
        }

        public Task<Subject> UpdateSubjectAsync(Subject subject)
        {
            subject.ThrowIfNull();

            _context.Subjects.Update(subject);
            _context.SaveChanges();

            return Task.FromResult(subject);
        }

        public Task<StudyGroup> GetGroupAsync(StudyGroupModel groupModel)
        {
            groupModel.ThrowIfNull();

            StudyGroup? group = _context.StudyGroups.AsEnumerable().SingleOrDefault(s => s.UniversityId.Equals(groupModel.UniversityId));

            if (group is null)
            {
                group = new StudyGroup(groupModel.UniversityId, groupModel.Name);
                _context.StudyGroups.Add(group);
                _context.SaveChanges();
            }

            return Task.FromResult(group);
        }

        public Task<StudyGroup> UpdateGroupAsync(StudyGroup group)
        {
            group.ThrowIfNull();

            _context.StudyGroups.Update(group);
            _context.SaveChanges();

            return Task.FromResult(group);
        }
    }
}