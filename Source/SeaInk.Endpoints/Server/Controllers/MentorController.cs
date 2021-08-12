using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.APIs;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;
using SeaInk.Core.APIs;
using SeaInk.Core.Entities;
using SeaInk.Endpoints.Shared.Dto;


namespace SeaInk.Endpoints.Server.Controllers
{
    [Route("[controller]")]
    public class MentorController: Controller
    {
        private readonly IUniversitySystemApi _api;

        public MentorController(IUniversitySystemApi universitySystemApi)
        {
            _api = universitySystemApi;
        }


        [HttpGet("{mentorId}/subjects")]
        public List<SubjectDto> GetSubjectsList(int mentorId)
        {
    
        Mentor mentor = _api.GetMentor(mentorId);
        if (mentor is null)
        {
            return new List<SubjectDto>();
        }

        List<Division> mentorDivisions = mentor.Divisions;
        return mentorDivisions.Select(x => new SubjectDto(x.Subject)).ToList();
        }
        
        [HttpGet("{mentorId}/subject/{subjectId}/groups")]
        public List<StudyGroupDto> GetGroupsList(int mentorId, int subjectId)
        {
            Mentor mentor = _api.GetMentor(mentorId);
            
            if (mentor is null)
            {
                return new List<StudyGroupDto>();
            }
            
            List<Division> mentorDivisions = _api.GetMentor(mentorId).Divisions;
            return mentorDivisions
                .Where(division => division.Subject.Id == subjectId)
                .SelectMany(division => division.Groups)
                .DistinctBy(group => group.Id)
                .Select(x => new StudyGroupDto(x))
                .ToList();
        }
    }
}