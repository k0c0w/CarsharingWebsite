using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinioConsumer.Features.OccasionAttachment.Query;
using System.Security.Claims;

namespace MinioConsumer.Features.OccasionAttachment;

[Route("attachments")]
public class OccasionAttachmentController : ControllerBase
{
    private readonly ISender _sender;

	public OccasionAttachmentController(ISender sender)
	{
		_sender = sender;
	}

	[HttpGet("{attachmentId:guid}")]
	public async Task<IActionResult> GetAttachmentInformationAsync([FromRoute] Guid attachmentId)
	{
		if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid applicantId))
			return NotFound();

		var getAttachmentMetadata = new GetOccasionAttachmentMetadataQuery(attachmentId, applicantId);
		var response = await _sender.Send(getAttachmentMetadata);

		return StatusCode((int)response.Code, response);
	}

	[HttpGet("{attachmentId:guid}/download/{attachmentFileName}")]
	public async Task<IResult> DownloadAttachmentFileAsync([FromRoute] Guid attachmentId, [FromRoute] string attachmentFileName)
	{
		if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid applicantId))
			return Results.NotFound(); 

		var downloadQuery = new DownloadOccasionAttachmentQuery(attachmentId, attachmentFileName, applicantId);
		var response = await _sender.Send(downloadQuery);

		if (!response)
            return Results.NotFound();

        var file = response.Value;
        return Results.Stream(file!.ContentStream, file.ContentType, file.Name);
    }


	[HttpPost("")]
	[Authorize]
	public Task<IActionResult> AppendAttachmentAsync(IEnumerable<IFormFile> files)
	{
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid applicantId))
            return Task.FromResult(BadRequest() as IActionResult);

        var createAttachmentCommand = new CreateOccasionAttachmentCommand(applicantId, files, applicantId);
        return OnCreateOccasionAttachmentAsync(createAttachmentCommand);
    }

    [HttpPost("/admin/attachments")]
    [Authorize(Roles = "Manager, Admin")]
    public Task<IActionResult> AppendAttachmentAsync(Guid occasionUserId, IEnumerable<IFormFile> files)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid applicantId))
            return Task.FromResult(BadRequest() as IActionResult);

		var createAttachmentCommand = new CreateOccasionAttachmentCommand(applicantId, files, occasionUserId);
		return OnCreateOccasionAttachmentAsync(createAttachmentCommand);
    }

	private async Task<IActionResult> OnCreateOccasionAttachmentAsync(CreateOccasionAttachmentCommand command)
	{
        var response = await _sender.Send(command);

		return StatusCode((int)response.Code, response);
    }
}
