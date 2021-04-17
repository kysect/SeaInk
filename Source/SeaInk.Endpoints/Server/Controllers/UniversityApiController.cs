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

        [HttpGet("mentor/{id}/subjects/")]
        public List<Subject> GetSubjectsByMentorId(Int32 id)
        {
            List<Division> mentorDivisions = _api.GetMentorBySystemId(id).Divisions;
            IEnumerable<Subject> subjectQuery =
                from division in mentorDivisions
                select division.Subject;
            return subjectQuery.ToList();
        }
        
        [HttpGet("mentor/{mid}/subject/{sid}/groups")]
        public List<StudyGroup> GetgroupsByMentorIdAndSubject(Int32 mid, Int32 sid)
        {
            List<Division> mentorDivisions = _api.GetMentorBySystemId(mid).Divisions;
            IEnumerable<List<StudyGroup>> groupsQuery =
                from division in mentorDivisions
                where division.Subject.Id == sid
                select division.Groups;
            List<StudyGroup> res = new List<StudyGroup>(0);
            foreach (var subj in groupsQuery) 
            { //if in 2 divisions the mentor teaches 2 subjects with the same id (this is unrealistic, if I get it right, so normally, cycle will work 1 time)
                res.AddRange(subj);
            } 
            return res;
        }
    }
}