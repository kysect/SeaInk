using System.Collections.Generic;
using System.Linq;
using Infrastructure.Database;
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
        public IActionResult GetCurrentMentor()
        {
            Mentor mentor = _databaseContext.Mentors.MaxBy(m => m.StudyGroupSubjects.Count);
            return Ok(mentor.ToDto());
        }

        [HttpGet("{mentorId:int}")]
        public IActionResult GetMentor(int mentorId)
        {
            Mentor mentor = _databaseContext.Mentors.Find(mentorId);
            if (mentor is null)
                return NotFound();

            return Ok(mentor.ToDto());
        }

        [HttpGet("{mentorId:int}/subjects")]
        public IActionResult GetSubjects(int mentorId)
        {
            Mentor mentor = _databaseContext.Mentors.Find(mentorId);
            if (mentor is null)
                return NotFound();

            List<SubjectDto> subjects = mentor.StudyGroupSubjects
                .Select(d => d.Subject)
                .DistinctBy(s => s.Id)
                .Select(s => s.ToDto())
                .ToList();

            return Ok(subjects);
        }
    }
}