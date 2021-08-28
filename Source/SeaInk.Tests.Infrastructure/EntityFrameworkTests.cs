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
    }
}