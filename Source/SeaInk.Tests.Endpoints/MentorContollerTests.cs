using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MoreLinq;
using NUnit.Framework;
using SeaInk.Core.Entities;
using SeaInk.Endpoints.Client.Controllers;
using SeaInk.Endpoints.Server;
using SeaInk.Endpoints.Shared.Dto;

namespace SeaInk.Tests.Endpoints
{
    public class MentorControllerTests
    {
        private MentorControllerClient _controller;
        private Mentor _mentor;

        [SetUp]
        public void Setup()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            HttpClient client = server.CreateClient();
            var databaseContext = server.Services.GetRequiredService<DatabaseContext>();
            
            _controller = new MentorControllerClient(client, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            _mentor = databaseContext.Mentors.First(m => m.Divisions.Count != 0);
        }

        [Test]
        public async Task MentorGetTest_MentorDtoFetchedFromClient()
        {
            MentorDto clientMentorDto = await _controller.GetMentorAsync(_mentor.Id);
            Assert.NotNull(clientMentorDto);
            Assert.AreEqual(_mentor.Id, clientMentorDto.Id);
        }

        [Test]
        public async Task SubjectsGetTest_SubjectListFetchedFromClient()
        {
            IReadOnlyList<SubjectDto> subjectsDtoClient = await _controller.GetSubjectsAsync(_mentor.Id);
            IReadOnlyList<SubjectDto> subjectsDto = _mentor.Divisions
                .Select(d => d.Subject)
                .DistinctBy(s => s.Id)
                .Select(s => s.ToDto())
                .ToList();

            Assert.AreEqual(subjectsDto.Count, subjectsDtoClient.Count);
            for (int i = 0; i < subjectsDto.Count; ++i)
            {
                Assert.AreEqual(subjectsDto[i].Id, subjectsDtoClient[i].Id);
            }
        }
    }
}