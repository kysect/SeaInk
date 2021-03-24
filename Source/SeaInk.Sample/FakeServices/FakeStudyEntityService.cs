using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bogus;
using SeaInk.Core.Entity;
using SeaInk.Core.Models;
using SeaInk.Core.Services;

namespace SeaInk.Sample.FakeServices
{
    public class FakeStudyEntityService
    {
        public List<Subject> GetSubjectsByMentorSystemId(int mentorSystemId)
        {
            return ListOf(SubjectFaker);
        }

        public List<StudyGroup> GetStudyGroupsBySystemSubjectId(string systemSubjectId)
        {
            return new List<StudyGroup>()
            {
                new StudyGroup(0, "M3113", 0, ListOf(StudentFaker, 20, 30,
                    faker => faker.RuleFor(s => s.SystemGroupId, 0))),
                new StudyGroup(1, "M3113", 0, ListOf(StudentFaker, 20, 30,
                    faker => faker.RuleFor(s => s.SystemGroupId, 1))),
                new StudyGroup(2, "M3113", 0, ListOf(StudentFaker, 20, 30,
                    faker => faker.RuleFor(s => s.SystemGroupId, 2)))
            };
        }

        public List<Student> GetStudentsBySystemGroupId(int systemGroupId)
        {
            return ListOf(StudentFaker, 20, 30, f =>
            {
                return f.RuleFor(s => s.SystemGroupId,
                    systemGroupId);
            });
        }


        private static readonly Faker<StudyAssignment> StudyAssignmentFaker = new Faker<StudyAssignment>()
            .RuleFor(m => m.LocalId,
                f => f.Random.Int(0, 5))
            .RuleFor(m => m.Title,
                f => f.Commerce.Department())
            .RuleFor(m => m.StartDate,
                f => f.Date.Past())
            .RuleFor(m => m.EndDate,
                f => f.Date.Future())
            .RuleFor(m => m.MinPoints,
                f => f.Random.Float(0, 5))
            .RuleFor(m => m.MaxPoints,
                f => f.Random.Float(5, 10));

        private static readonly Faker<Subject> SubjectFaker = new Faker<Subject>()
            .RuleFor(p => p.Title,
                f => f.Commerce.Department())
            .RuleFor(p => p.StartDate,
                f => f.Date.Past())
            .RuleFor(p => p.EndDate,
                f => f.Date.Future())
            .RuleFor(p => p.AssignmentIds,
                f =>
                {
                    var random = new Random();
                    var list = new List<int>();

                    for (var i = 0; i < random.Next(3, 5); ++i)
                    {
                        list.Add(i);
                    }

                    return list;
                });

        private static readonly Faker<Student> StudentFaker = new Faker<Student>()
            .RuleFor(s => s.SystemId,
                f => f.IndexFaker)
            .RuleFor(s => s.SystemGroupId,
                f => f.IndexFaker)
            .RuleFor(s => s.Token,
                f => f.Internet.Password())
            .RuleFor(s => s.FirstName,
                f => f.Person.FirstName)
            .RuleFor(s => s.LastName,
                f => f.Person.LastName)
            .RuleFor(s => s.MidName,
                f => f.Person.UserName)
            .RuleFor(s => s.SystemGroupId,
                f => f.Random.Int(0, 2));

        private static List<T> ListOf<T>(Faker<T> faker, int l = 10, int h = 20,
            Func<Faker<T>, Faker<T>> lambda = null) where T : class
        {
            var list = new List<T>();
            var random = new Random();

            for (var i = 0; i < random.Next(l, h); ++i)
            {
                list.Add(lambda == null ? faker.Generate() : lambda(faker).Generate());
            }

            return list;
        }
    }
}