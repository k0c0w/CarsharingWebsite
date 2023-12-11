﻿using Features.Occasion.Admin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Entities.Entities;
using Persistence.RepositoryImplementation;

namespace Carsharing.Controllers.Admin;

[Route("api/admin/occasions")]
[Authorize(Roles = "Manager, Admin")]
public class AdminOccasionController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly OccasionMessageRepository _occasionMessageRepository; 

    public AdminOccasionController(ISender mediator, OccasionMessageRepository occasionMessageRepository)
    {
        _mediator = mediator;
        _occasionMessageRepository = occasionMessageRepository;
    }

    [HttpGet("uncompleted")]
    public async Task<IActionResult> GetAllOpenOccasionsAsync()
    {
        var getAllOpenedOccasionsQuery = new GetOpenedOccasionsQuery();

        var occasionsResult = await _mediator.Send(getAllOpenedOccasionsQuery);

        if (!occasionsResult)
            return BadRequest();

        Debug.Assert(occasionsResult.Value != null);
        return new JsonResult(occasionsResult.Value.Select(x => new { x.Id, OccasionType = x.OccasionType.ToString(), x.Topic }));
    }

    [HttpGet("{guid:guid}")]
    public async Task<IActionResult> GetOcassionInfoByIdAsync([FromRoute] Guid guid)
    {
        var getAllOpenedOccasionsQuery = new GetOccasionQuery(guid);

        var occasionsResult = await _mediator.Send(getAllOpenedOccasionsQuery);

        if (!occasionsResult)
            return BadRequest();

        var occasion = occasionsResult.Value;
        if (occasion is null)
            return NotFound();

        return new JsonResult(new
        {
            occasion.Id,
            occasion.Topic,
            OccasionType = occasion.OccasionType.ToString(),
            IssuerId = occasion.IssuerId,
        });
    }

    [HttpPost("{occasionId:guid}/complete")]
    public async Task<IActionResult> CloseOccasionAsync([FromRoute] Guid occasionId)
    {
        var completeOccassionCommand = new CompleteOccasionCommand(occasionId);

        var completionResult = await _mediator.Send(completeOccassionCommand);

        if (!completionResult)
            return BadRequest(completionResult);

        return Ok();
    }

    [HttpPost("send_message")]
    [AllowAnonymous]
    public async Task<IActionResult> SendMessage(OccasionMessage message)
    {
        await _occasionMessageRepository.AddAsync(message);

        var result = await _occasionMessageRepository.GetMessagesAsync(message.OccasionId, 100, 0);

        return Ok();
    }
}