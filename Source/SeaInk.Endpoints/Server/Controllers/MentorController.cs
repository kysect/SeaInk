using System.Collections.Generic;
using System.Linq;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using SeaInk.Core.Entities;
using SeaInk.Endpoints.Shared.Dto;

namespace SeaInk.Endpoints.Server.Controllers
{
    [Route("[controller]")]
    public class MentorController: Controller
    {
        private readonly DatabaseContext _databaseContext;

        public MentorController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        [HttpGet("{mentorId}")]
        public MentorDto GetMentor(int mentorId)
        {
            Mentor mentor = _databaseContext.Mentors.Find(mentorId);
            return mentor?.ToDto();
        }
        
        [HttpGet("{mentorId}/subjects")]
        public IReadOnlyList<SubjectDto> GetSubjects(int mentorId)
        {
            Mentor mentor = _databaseContext.Mentors.Find(mentorId);
            if (mentor is null)
                return new List<SubjectDto>();

            List<Division> divisions = mentor.Divisions;
            return divisions.Select(x => x.Subject.ToDto()).ToList();
        }

        [HttpGet("{mentorId}/subjects/{subjectId}/divisions")]
        public IReadOnlyList<DivisionDto> GetDivisions(int mentorId, int subjectId)
        {
            Mentor mentor = _databaseContext.Mentors.Find(mentorId);
            if (mentor is null)
                return new List<DivisionDto>();

            List<Division> divisions = mentor.Divisions.Where(d => d.Subject.Id == subjectId).ToList();
            return divisions.Select(d => d.ToDto()).ToList();
        }

        [HttpGet("{mentorId}/subjects/{subjectId}/divisions/{divisionId}/groups")]
        public IReadOnlyList<StudyGroupDto> GetGroups(int mentorId, int subjectId, int divisionId)
        {
            Mentor mentor = _databaseContext.Mentors.Find(mentorId);
            if (mentor is null)
                return new List<StudyGroupDto>();

            Division division = mentor.Divisions.SingleOrDefault(d => d.Id == divisionId && d.Subject.Id == subjectId);
            if (division is null)
                return new List<StudyGroupDto>();

            return division.Groups.Select(g => g.ToDto()).ToList();
        }
    }
}