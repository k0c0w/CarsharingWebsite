using Features.Occasion;
using Features.Occasion.Inputs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Carsharing.Controllers.Api;

[Authorize]
[Route("api/[controller]")]
public class OccasionController : ControllerBase
{
    private readonly ISender _mediator;

    public OccasionController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyOpenedOccasionAsync() 
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var getMyOccasionQuery = new GetOpenedUserOccasionQuery(userId);

        var occasionQuery = await _mediator.Send(getMyOccasionQuery);

        if (occasionQuery)
        {
            if(occasionQuery.Value is null)
                return NotFound();

            return Ok(occasionQuery.Value);
        }

        return BadRequest(occasionQuery.ErrorMessage);
    }

    [HttpPost("")]
    public async Task<IActionResult> OpenNewOccasionAsync([FromBody] CreateOccasionDto occasionInfo)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
}
