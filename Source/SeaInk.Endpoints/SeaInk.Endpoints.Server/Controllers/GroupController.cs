using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeaInk.Application.Commands;
using SeaInk.Application.Queries;
using SeaInk.Application.Services;
using SeaInk.Core.Models;
using SeaInk.Core.StudyTable;
using SeaInk.Endpoints.Common.Requests;
using SeaInk.Utility.Extensions;

namespace SeaInk.Endpoints.Server.Controllers;

[ApiController]
[Route("groups")]
public class GroupController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;

    public GroupController(IMediator mediator, IIdentityService identityService)
    {
        _identityService = identityService;
        _mediator = mediator.ThrowIfNull();
    }

    [HttpGet("groups/{studyStudentGroupId:guid}/sheet")]
    public async Task<IActionResult> GetMentorStudentGroupSheet(Guid studyStudentGroupId)
    {
        var query = new GetStudyStudentGroupSheet.Query(await _identityService.GetCurrentMentor(), studyStudentGroupId);
        SheetLink response = await _mediator.Send(query).ConfigureAwait(false);
        return Ok(response);
    }

    [HttpPost("groups/table/create")]
    public async Task<IActionResult> CreateStudyStudentGroupTable([FromBody] CreateStudyStudentGroupTableRequest request)
    {
        var command = new CreateStudyStudentGroupTable.Command(
            await _identityService.GetCurrentMentor(), request.StudyStudentGroupId, request.Layout);
        CreateSheetResponse response = await _mediator.Send(command).ConfigureAwait(false);
        return Ok(response);
    }

    [HttpPut("groups/table/difference/calculate")]
    public async Task<IActionResult> CalculateStudyStudentGroupTableDifference([FromBody] CalculateStudyStudentGroupTableDifferenceRequest request)
    {
        var command = new CalculateStudyStudentGroupTableDifference.Command(await _identityService.GetCurrentMentor(), request.StudyStudentGroupId);
        StudentAssignmentProgressTableDifference response = await _mediator.Send(command).ConfigureAwait(false);
        return Ok(response);
    }

    [HttpPut("groups/table/difference/apply")]
    public async Task<IActionResult> AppleStudyStudentGroupTableDifference([FromBody] ApplyStudyStudentGroupTableDifferenceRequest request)
    {
        var command = new ApplyStudyStudentGroupTableDifference.Command(
            await _identityService.GetCurrentMentor(), request.StudentStudentGroupId, request.Difference);
        await _mediator.Send(command).ConfigureAwait(false);
        return Ok();
    }
}