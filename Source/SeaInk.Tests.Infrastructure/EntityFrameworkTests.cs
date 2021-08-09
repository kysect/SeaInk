using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.APIs;
using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SeaInk.Core.APIs;
using SeaInk.Core.Entities;
using SeaInk.Core.Repositories;
using SeaInk.Tests.Infrastructure.Extensions;

namespace SeaInk.Tests.Infrastructure
{
    public class EntityFrameworkTests
    {
        private IServiceProvider _provider;
        private ITestUniversitySystemApi _api;

        [SetUp]
        public void Setup()
        {
            _provider = new ServiceCollection()
                .AddTestServices()
                .BuildServiceProvider();

            if (_provider.GetRequiredService<IUniversitySystemApi>() is not ITestUniversitySystemApi api)
                throw new ArgumentException("Using not testing API in testing");

            _api = api;

            _api.Log += Console.WriteLine;
        }

        [Test]
        public void StudentAssignmentProgressRepositoryTest_SAP_AddedToDatabase()
        {
            var repository = _provider.GetRequiredService<IEntityRepository<StudentAssignmentProgress>>();
            
            StudentAssignmentProgress progress = _api.StudentAssignmentProgresses.First();
            repository.Create(progress);
            
            StudentAssignmentProgress createdProgress = repository[progress.Id];

            Assert.NotNull(createdProgress);
            Assert.AreEqual(progress.Id, createdProgress.Id);
            Assert.AreEqual(progress.Assignment.Id, createdProgress.Assignment.Id);
            Assert.AreEqual(progress.Progress, createdProgress.Progress);
            Assert.AreEqual(progress.Student.Id, createdProgress.Student.Id);
        }

        [Test]
        public void DivisionRepositoryTest_DivisionAddedToDatabase()
        {
            var repository = _provider.GetRequiredService<IEntityRepository<Division>>();
            
            Division division = _api.Divisions.First();
            repository.Create(division);

            Division createdDivision = repository[division.Id];
            
            Assert.NotNull(division);
            Assert.AreEqual(division.Id, createdDivision.Id);
            Assert.AreEqual(division.Mentor.Id, createdDivision.Mentor.Id);
            Assert.AreEqual(division.Mentor.UniversityId, createdDivision.Mentor.UniversityId);
            Assert.AreEqual(division.Subject.Id, createdDivision.Subject.Id);
            Assert.AreEqual(division.Subject.UniversityId, createdDivision.Subject.UniversityId);
            Assert.True(division.Groups.ToIds().ToHashSet()
                            .SetEquals(createdDivision.Groups.ToIds()));

            List<(StudyGroup found, StudyGroup created)> matchedGroups = division.Groups
                .Select(f => (f, createdDivision.Groups.Single(c => c.Id == f.Id))).ToList();

            foreach (var groupPair in matchedGroups)
            {
                Assert.AreEqual(groupPair.found.Name, groupPair.created.Name);
                Assert.AreEqual(groupPair.found.Admin.Id, groupPair.created.Admin.Id);
                Assert.AreEqual(groupPair.found.UniversityId, groupPair.created.UniversityId);
                Assert.True(groupPair.found.Students.ToIds().ToHashSet()
                                .SetEquals(groupPair.created.Students.ToIds()));

                List<(Student found, Student created)> matchedStudents = groupPair.found.Students
                    .Select(f => (f, groupPair.created.Students.Single(c => c.Id == f.Id))).ToList();

                foreach (var studentPair in matchedStudents)
                {
                    Assert.AreEqual(studentPair.found.FirstName, studentPair.created.FirstName);
                    Assert.AreEqual(studentPair.found.MidName, studentPair.created.MidName);
                    Assert.AreEqual(studentPair.found.LastName, studentPair.created.LastName);
                    Assert.AreEqual(studentPair.found.UniversityId, studentPair.created.UniversityId);
                }
            }
        }
    }
}