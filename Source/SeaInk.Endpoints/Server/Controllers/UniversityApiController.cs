using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;
using SeaInk.Core;
using SeaInk.Core.Entities;


namespace SeaInk.Endpoints.Server.Controllers
{
    [Route("[Controller]")]
    public class ApiController:Controller
    {
        private readonly IUniversitySystemApi _api;

        public ApiController()
        {
            _api = new FakeUniversitySystemApi();
        }

        [HttpGet("mentors/{mentorId}/subjects")]
        public List<Subject> GetSubjectsList(int mentorId)
        {
            List<Division> mentorDivisions = _api.GetMentorBySystemId(mentorId).Divisions;
            return mentorDivisions.Select(x => x.Subject).ToList();
        }
        
        [HttpGet("mentors/{mentorId}/subjects/{subjectId}/groups")]
        public List<StudyGroup> GetGroupsList(int mentorId, int subjectId)
        {
            List<Division> mentorDivisions = _api.GetMentorBySystemId(mentorId).Divisions;
            return mentorDivisions
                .Where(division => division.Subject.Id == subjectId)
                .SelectMany(division => division.Groups)
                .DistinctBy(group => group.SystemId)
                .ToList();
        }
    }
}