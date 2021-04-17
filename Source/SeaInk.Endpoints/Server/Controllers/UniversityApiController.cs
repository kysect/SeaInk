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

        [HttpGet("mentor/{id}/subjects")]
        public List<Subject> GetSubjectsByMentorId(int id)
        {
            List<Division> mentorDivisions = _api.GetMentorBySystemId(id).Divisions;
            return mentorDivisions.Select( x => x.Subject).ToList();
        }
        
        [HttpGet("mentor/{mid}/subject/{sid}/groups")]
        public List<StudyGroup> GetgroupsByMentorIdAndSubject(int mid, int sid)
        {
            List<Division> mentorDivisions = _api.GetMentorBySystemId(mid).Divisions;
            return mentorDivisions
                .Where(division => division.Subject.Id == sid)
                .SelectMany(division => division.Groups)
                .DistinctBy(group => group.SystemId)
                .ToList();
        }
    }
}