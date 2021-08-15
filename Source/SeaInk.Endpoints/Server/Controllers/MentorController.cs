using System.Collections.Generic;
using System.Linq;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;
using SeaInk.Core.Entities;
using SeaInk.Endpoints.Shared.Dto;


namespace SeaInk.Endpoints.Server.Controllers
{
    [Route("[controller]")]
    public class MentorController : Controller
    {
        private readonly DatabaseContext _db;

        public MentorController(DatabaseContext db)
        {
            _db = db;
        }


        [HttpGet("{mentorId}/subjects")]
        public List<SubjectDto> GetSubjectsList(int mentorId)
        {
            Mentor mentor = _db.Mentors.Find(mentorId);

            if (mentor is null)
                return new List<SubjectDto>();

            List<Division> mentorDivisions = mentor.Divisions;
            return mentorDivisions.Select(x => x.Subject.ToDto()).ToList();
        }

        [HttpGet("{mentorId}/subject/{subjectId}/groups")]
        public List<StudyGroupDto> GetGroupsList(int mentorId, int subjectId)
        {
            Mentor mentor = _db.Mentors.Find(mentorId);

            if (mentor is null)
                return new List<StudyGroupDto>();

            List<Division> mentorDivisions = mentor.Divisions;
            return mentorDivisions
                .Where(division => division.Subject.Id == subjectId)
                .SelectMany(division => division.Groups)
                .DistinctBy(group => group.Id)
                .Select(x => x.ToDto())
                .ToList();
        }
    }
}