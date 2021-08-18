using System.Collections.Generic;
using System.Linq;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;
using SeaInk.Core.Entities;
using SeaInk.Endpoints.Shared.Dto;

namespace SeaInk.Endpoints.Server.Controllers
{
    [Route("[controller]s")]
    public class MentorController: Controller
    {
        private readonly DatabaseContext _databaseContext;

        public MentorController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet("current")]
        public MentorDto GetCurrentMentor()
        {
            Mentor mentor = _databaseContext.Mentors
                .First(m => m.Divisions.Count > 1 && m.Divisions.Any(d => d.Groups.Count > 1));
            return mentor.ToDto();
        }

        [HttpGet("{mentorId}")]
        public MentorDto GetMentor(int mentorId)
        {
            Mentor mentor = _databaseContext.Mentors.Find(mentorId);
            return mentor?.ToDto();
        }

        [HttpGet("{mentorId}/subjects")]
        public IReadOnlyList<SubjectDto> GetSubjects(int mentorId)
        {
            Mentor mentor = _databaseContext.Mentors.Find(mentorId);
            if (mentor is null)
                return new List<SubjectDto>();

            List<SubjectDto> subjects = mentor.Divisions
                .Select(d => d.Subject)
                .DistinctBy(s => s.Id)
                .Select(s => s.ToDto())
                .ToList();

            return subjects;
        }
    }
}