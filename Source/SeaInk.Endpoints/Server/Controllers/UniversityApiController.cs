using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;
using SeaInk.Core.APIs;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Server.Controllers
{
    [Route("[controller]")]
    public class ApiController: Controller
    {
        private readonly IUniversitySystemApi _api;

        public ApiController(IUniversitySystemApi universitySystemApi)
        {
            _api = universitySystemApi;
        }

        [HttpGet("mentors/{mentorId}/subjects")]
        public List<Subject> GetSubjectsList(int mentorId)
        {
            List<Division> mentorDivisions = _api.GetMentor(mentorId).Divisions;
            return mentorDivisions.Select(x => x.Subject).ToList();
        }

        [HttpGet("mentors/{mentorId}/subjects/{subjectId}/groups")]
        public List<StudyGroup> GetGroupsList(int mentorId, int subjectId)
        {
            List<Division> mentorDivisions = _api.GetMentor(mentorId).Divisions;
            return mentorDivisions
                .Where(division => division.Subject.Id == subjectId)
                .SelectMany(division => division.Groups)
                .DistinctBy(group => group.Id)
                .ToList();
        }
    }
}