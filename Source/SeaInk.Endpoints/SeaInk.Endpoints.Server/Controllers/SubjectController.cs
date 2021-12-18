using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeaInk.Application.Queries;
using SeaInk.Application.Services;
using SeaInk.Infrastructure.Dto;
using SeaInk.Utility.Extensions;

namespace SeaInk.Endpoints.Server.Controllers;

[ApiController]
[Route("subjects")]
public class SubjectController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;

    public SubjectController(IMediator mediator, IIdentityService identityService)
    {
        _identityService = identityService;
        _mediator = mediator.ThrowIfNull();
    }

    [HttpGet]
    public async Task<IActionResult> GetMentorSubjects()
    {
        var query = new GetMentorSubjects.Query(await _identityService.GetCurrentMentor());
        IReadOnlyCollection<SubjectDto> response = await _mediator.Send(query).ConfigureAwait(false);
        return Ok(response);
    }

    [HttpGet("{subjectId:guid}/groups")]
    public async Task<IActionResult> GetMentorSubjectGroups(Guid subjectId)
    {
        var query = new GetMentorSubjectStudyStudentGroups.Query(await _identityService.GetCurrentMentor(), subjectId);
        IReadOnlyCollection<StudyStudentGroupDto> response = await _mediator.Send(query).ConfigureAwait(false);
        return Ok(response);
    }
}