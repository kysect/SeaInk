using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.APIs;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SeaInk.Core.Entities;
using SeaInk.Tests.Infrastructure.Extensions;

namespace SeaInk.Tests.Infrastructure
{
    public class EntityFrameworkTests
    {
        private DatabaseContext _context;
        private ITestUniversitySystemApi _api;

        [SetUp]
        public void Setup()
        {
            _context = DatabaseContext.GetTestContext("SeaInk");
            _api = new FakeUniversitySystemApi();

            _api.Log += Console.WriteLine;
        }

        [Test]
        public void StudentAssignmentProgressRepositoryTest_SAP_AddedToDatabase()
        {
            DbSet<StudentAssignmentProgress> repository = _context.StudentAssignmentProgresses;
            
            StudentAssignmentProgress progress = _api.StudentAssignmentProgresses.First();
            repository.Add(progress);

            StudentAssignmentProgress createdProgress = repository.Find(progress.Id);

            Assert.NotNull(createdProgress);
            Assert.AreEqual(progress.Id, createdProgress.Id);
            Assert.AreEqual(progress.Assignment.Id, createdProgress.Assignment.Id);
            Assert.AreEqual(progress.Progress, createdProgress.Progress);
            Assert.AreEqual(progress.Student.Id, createdProgress.Student.Id);
        }

        [Test]
        public void DivisionRepositoryTest_DivisionAddedToDatabase()
        {
            DbSet<Division> repository = _context.Divisions;
            
            Division division = _api.Divisions.First();
            repository.Add(division);

            Division createdDivision = repository.Find(division.Id);
            
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