﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinioConsumer.Features.OccasionAttachment.Query;
using System.Security.Claims;
using CommonExtensions.Claims;
using ActionResults = Microsoft.AspNetCore.Http.Results;

namespace MinioConsumer.Features.OccasionAttachment;

[Route("attachments")]
[Authorize]
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
        if (!Guid.TryParse(User.GetId(), out Guid applicantGuid))
			return NotFound();

		var getAttachmentMetadata = new GetOccasionAttachmentMetadataQuery(attachmentId, applicantGuid);
		var response = await _sender.Send(getAttachmentMetadata);

		return StatusCode((int)response.Code, response);
	}

	[HttpGet("{attachmentId:guid}/download/{attachmentFileName}")]
	[AllowAnonymous]
	public async Task<IResult> DownloadAttachmentFileAsync([FromRoute] Guid attachmentId, [FromRoute] string attachmentFileName)
	{
		var applicantId = User.GetId();

        if (!Guid.TryParse(applicantId, out Guid applicantGuid))
			return ActionResults.NotFound(); 

		var downloadQuery = new DownloadOccasionAttachmentQuery(attachmentId, attachmentFileName, applicantGuid);
		var response = await _sender.Send(downloadQuery);

		if (!response)
            return ActionResults.NotFound();

        var file = response.Value;
        return ActionResults.Stream(file!.ContentStream, file.ContentType, file.Name);
    }


	[HttpPost("")]
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
