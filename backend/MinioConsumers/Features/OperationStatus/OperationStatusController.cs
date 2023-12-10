using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinioConsumer.Services.Repositories;
using System.Security.Claims;

namespace MinioConsumer.Features.OperationStatus;

[Route("operation")]
[Authorize]
public class OperationStatusController : ControllerBase
{
    private readonly OperationRepository _operationRepository;
    private readonly ILogger<Exception> _logger;

    public OperationStatusController(OperationRepository repository, ILogger<Exception> logger)
    {
        _operationRepository = repository;
        _logger = logger;
    }

    [HttpGet("{guid:guid}/status")]
    public async Task<IActionResult> GetOperationStatusAsync([FromRoute] Guid guid)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid applicantId))
            return BadRequest();
        try
        {
            var operation = await _operationRepository.GetByIdAsync(guid);
            if (operation is null || operation.InitializerUserId != applicantId)
                return NotFound();

            return new JsonResult(new 
            {
                status = operation.OperationStatus.ToString()
            });
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound();
        }
    }
}
