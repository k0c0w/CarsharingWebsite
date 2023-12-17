using Features.Occasion.Admin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Domain.Common;
using Entities.Entities;
using Features.Occasion;
using Features.Occasion.Inputs;
using Persistence.Chat.ChatEntites.Dtos;
using Persistence.RepositoryImplementation;

namespace Carsharing.Controllers.Admin;

[Route("api/admin/occasions")]
[Authorize(Roles = "Manager, Admin")]
public class AdminOccasionController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMessageProducer _publisher; //TODO: Удалить 
    private readonly OccasionMessageRepository _occasionMessageRepository; 

    public AdminOccasionController(ISender mediator, OccasionMessageRepository occasionMessageRepository, IMessageProducer publisher)
    {
        _mediator = mediator;
        _occasionMessageRepository = occasionMessageRepository;
        _publisher = publisher;
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
    
    // -------------------- //
    
    [HttpPost("send_message")]
    public async Task<IActionResult> Create([FromBody] OccasionChatMessageDto message)
    {
        try
        {
            await _publisher.SendMessageAsync(new OccasionChatMessageDto()
            {
                AuthorId = User.GetId(),
                Attachment = message.Attachment,
                OccasionId = message.OccasionId,
                Text = message.Text,
                Time = message.Time,
                IsAuthorManager = true,
            });
            
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("create_occasion")]
    public async Task<IActionResult> CreateOccasion(Guid userId, string topic)
    {
        var result = await _mediator.Send(new CreateOccasionCommand(userId, 
            new CreateOccasionDto() {
                Topic = topic, OccasionType = nameof(OccasionTypeDefinition.Other)
            } ));

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
    }
}