using System.Collections.Generic;
using System.Linq;
using Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;
using SeaInk.Core.Entities;
using SeaInk.Endpoints.Shared.Dto;

namespace SeaInk.Endpoints.Server.Controllers
{
    [Route("mentors")]
    public class MentorController: Controller
    {
        private readonly DatabaseContext _databaseContext;

        public MentorController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        //TODO: Proper current mentor
        [HttpGet("current")]
        public MentorDto GetCurrentMentor()
        {
            Mentor mentor = _databaseContext.Mentors.MaxBy(m => m.Divisions.Count);
            return mentor.ToDto();
        }

        [HttpGet("{mentorId}")]
        public MentorDto GetMentor(int mentorId)
        {
            Mentor mentor = _databaseContext.Mentors.Find(mentorId);
            if (mentor is null)
                throw new BadHttpRequestException("Mentor with given id not found");

            return mentor.ToDto();
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