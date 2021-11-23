using System.Collections.Generic;
using System.Linq;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using SeaInk.Core.Entities;
using SeaInk.Endpoints.Shared.Dto;

namespace SeaInk.Endpoints.Server.Controllers
{
    [Route("subjects")]
    public class SubjectController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public SubjectController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet("{subjectId:int}/groups")]
        public ActionResult<List<StudyGroupSubjectDto>> GetGroups(int subjectId)
        {
            List<StudyGroupSubjectDto> result = _databaseContext
                .StudyGroupSubjects
                .Where(sgs => sgs.Subject.Id == subjectId)
                .Select(sgs => sgs.ToDto())
                .ToList();

            return Ok(result);
        }

        [HttpGet("{subjectId:int}/groups/{groupId:int}/generate-table")]
        public ActionResult<StudyGroupSubjectDto> GenerateTable(int subjectId, int groupId)
        {
            StudyGroupSubject studyGroupSubject = _databaseContext
                .StudyGroupSubjects
                .Single(sgs => sgs.Subject.Id == subjectId && sgs.StudyGroup.Id == groupId);

            //TODO: add table generation

            return Ok(studyGroupSubject);
        }
    }
}
