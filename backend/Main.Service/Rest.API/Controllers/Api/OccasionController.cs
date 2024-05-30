using Features.Occasion;
using Features.Occasion.Inputs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entities.Entities;
using CommonExtensions.Claims;

namespace Carsharing.Controllers.Api;

[Authorize]
[Route("api/[controller]")]
public class OccasionsController : ControllerBase
{
    private readonly ISender _mediator;

    public OccasionsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyOpenedOccasionAsync() 
    {
        var userId = User.GetId();

        var getMyOccasionQuery = new GetOpenedUserOccasionQuery(userId);

        var occasionQuery = await _mediator.Send(getMyOccasionQuery);

        if (occasionQuery)
        {
            if(occasionQuery.Value is null || occasionQuery.Value == Guid.Empty)
                return NotFound();

            return Ok(occasionQuery.Value);
        }

        return BadRequest(occasionQuery.ErrorMessage);
    }

    [HttpPost("")]
    public async Task<IActionResult> OpenNewOccasionAsync([FromBody] CreateOccasionDto occasionInfo)
    {
        var userId = User.GetId();

        if (!Guid.TryParse(userId, out Guid issuerId))
            return BadRequest();

        var createOccasionCommand = new CreateOccasionCommand(issuerId, occasionInfo);

        var creationResult = await _mediator.Send(createOccasionCommand);

        if (creationResult)
        {
            return Ok(creationResult.Value);
        }

        return BadRequest(creationResult.ErrorMessage);
    }

    [HttpGet("types")]
    public IActionResult GetOccasionTypes()
    {
        return Ok(new string[]
        {
            OccasionTypeDefinition.RoadAccident.ToString(),
            OccasionTypeDefinition.VehicleBreakdown.ToString(),
            OccasionTypeDefinition.Other.ToString()
        });
    }

    [HttpGet("{guid:guid}/chat")]
    public async Task<IActionResult> GetMessagesRelatedToOccasionAsync([FromRoute] Guid guid)
    {
        var userId = HttpContext.User.GetId();
        var query = new GetOccasionMessagesQuery(guid, userId);

        var response = await _mediator.Send(query);
        if (response)
            return new JsonResult(response.Value);

        return NotFound();
    }
}
