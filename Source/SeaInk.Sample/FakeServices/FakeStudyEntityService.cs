using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bogus;
using SeaInk.Core.Entity;
using SeaInk.Core.Entity.Base;
using SeaInk.Core.Services;

namespace SeaInk.Sample.FakeServices
{
    public class FakeStudyEntityService : StudyEntityService
    {
        public override List<Core.Entity.Base.Program> GetSubjectsByCurator(int curatorId)
        {
            var list = new List<Core.Entity.Base.Program>();
            var random = new Random();
            var fakeSubject = new Faker<Core.Entity.Base.Program>()
                .RuleFor(p => p.Title,
                    f => f.Commerce.Department())
                .RuleFor(p => p.Begin,
                    f => f.Date.Past())
                .RuleFor(p => p.End,
                    f => f.Date.Future())
                .RuleFor(p => p.Milestones,
                    GetMilestones());

            for (var i = 0; i < random.Next(10, 20); ++i)
            {
                list.Add(fakeSubject.Generate());
            }

            return list;
        }

        public List<Milestone> GetMilestones()
        {
            var fakeMileStones = new Faker<Milestone>()
                .RuleFor(m => m.Title,
                    f => f.Commerce.Department())
                .RuleFor(m => m.Begin,
                    f => f.Date.Past())
                .RuleFor(m => m.End,
                    f => f.Date.Future())
                .RuleFor(m => m.Minimum,
                    f => f.Random.Float(0, 5))
                .RuleFor(m => m.Maximum,
                    f => f.Random.Float(5, 10));

            var list = new List<Milestone>();
            var random = new Random();

            for (int i = 0; i < random.Next(1, 5); ++i)
            {
                list.Add(fakeMileStones.Generate());
            }

            return list;
        }

        public override List<CuratedGroup> GetCuratedGroupsBySubject(string subject)
        {
            var groups = new List<UniversityGroup>(3);

            groups[0].Name = "M3113";
            groups[0].Members = GetStudentsByUniversityGroup(groups[0]);
            
            groups[1].Name = "M3114";
            groups[1].Members = GetStudentsByUniversityGroup(groups[1]);
            
            groups[2].Name = "M3115";
            groups[2].Members = GetStudentsByUniversityGroup(groups[2]);

            var curator = new Curator();
            curator.Name = "Повыш";
            curator.Token = "1337";

            var list = new List<CuratedGroup>();
            var random = new Random();
            Func<List<Student>> pack = () =>  
                groups[0].Members.Where(x => random.Next(0, 1) == 1)
                .Concat(groups[1].Members.Where(x => random.Next(0, 1) == 1))
                .Concat(groups[2].Members.Where(x => random.Next(0, 1) == 1).ToArray()).ToList();

            var fakeGroup = new Faker<CuratedGroup>()
                .RuleFor(g => g.Members,
                    f => pack())
                .RuleFor(g => g.Curator,
                    curator)
                .RuleFor(g => g.Title,
                    subject)
                .RuleFor(g => g.Program,
                    GetSubjectsByCurator(0)[0]);

            for (int i = 0; i < random.Next(2, 3); i++)
            {
                list.Add(fakeGroup.Generate());
            }

            return list;
        }

        public override List<Student> GetStudentsByUniversityGroup(UniversityGroup group)
        {
            var list = new List<Student>();
            var random = new Random();
            var fakeStudent = new Faker<Student>()
                .RuleFor(s => s.Group,
                    group)
                .RuleFor(s => s.Identifier,
                    f => f.IndexFaker)
                .RuleFor(s => s.Name,
                    f => f.Person.FullName);

            for (int i = 0; i < random.Next(5, 10); ++i)
            {
                list.Add(fakeStudent.Generate());
            }

            return list;
        }
    }
}