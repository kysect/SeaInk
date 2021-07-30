using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;
using SeaInk.Core;
using SeaInk.Core.Entities;
using SeaInk.Endpoints.Shared.Dto;


namespace SeaInk.Endpoints.Server.Controllers
{
    [Route("[Controller]")]
    public class MentorController:Controller
    {
        private readonly IUniversitySystemApi _api;

        public MentorController()
        {
            _api = new FakeUniversitySystemApi();
        }

        [HttpGet("{mentorId}/subjects")]
        public List<SubjectDto> GetSubjectsList(int mentorId)
        {
            List<Division> mentorDivisions = _api.GetMentorBySystemId(mentorId).Divisions;
            return mentorDivisions.Select(x => new SubjectDto(x.Subject)).ToList();
        }
        
        [HttpGet("{mentorId}/subject/{subjectId}/groups")]
        public List<StudyGroupDto> GetGroupsList(int mentorId, int subjectId)
        {
            List<Division> mentorDivisions = _api.GetMentorBySystemId(mentorId).Divisions;
            return mentorDivisions
                .Where(division => division.Subject.Id == subjectId)
                .SelectMany(division => division.Groups)
                .DistinctBy(group => group.SystemId)
                .Select(x => new StudyGroupDto(x))
                .ToList();
        }
    }
}