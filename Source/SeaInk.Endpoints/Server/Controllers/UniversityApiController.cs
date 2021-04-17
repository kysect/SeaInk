using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        public List<Subject> GetSubjectsByMentorId(Int32 id)
        {
            List<Division> mentorDivisions = _api.GetMentorBySystemId(id).Divisions;
            return mentorDivisions.Select( x => x.Subject).ToList();
        }
        
        [HttpGet("mentor/{mid}/subject/{sid}/groups")]
        public List<StudyGroup> GetgroupsByMentorIdAndSubject(Int32 mid, Int32 sid)
        {
            List<Division> mentorDivisions = _api.GetMentorBySystemId(mid).Divisions;
            return mentorDivisions.Aggregate(new List<StudyGroup>(), (acc, y) =>
            {
                if (y.Subject.Id == sid)
                    acc.AddRange(y.Groups);
                return acc;
            });
        }
    }
}