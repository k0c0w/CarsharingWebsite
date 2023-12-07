using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinioConsumer.Features.Documents.InputModels;
using MinioConsumer.Features.Documents.Query;

namespace MinioConsumer.Features.Documents;

[Route("documents")]
//[Authorize(Roles = "Admin")]
public class DocumentsController : ControllerBase
{
	private readonly IMediator _mediator;

	public DocumentsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<IActionResult> Documents(DocumentInfoDto documentInfo , IFormFile document)
	{
		/*
		//todo: parse user id
		if (!Guid.TryParse(HttpContext.User.Identity.Name, out Guid userId))
			return  StatusCode(403, (new HttpResponse(System.Net.HttpStatusCode.Forbidden, error: "Authentication required.")));
		*/
		var userId = Guid.NewGuid();
		var command = new CreateDocumentCommand(documentInfo.IsPrivate, documentInfo.Annotation, userId, document);

		var response = await _mediator.Send(command);
		if (response)
			return Accepted(response);

		return StatusCode((int)response.Code, response);
	}

	[HttpGet("{id:guid}")]
	[AllowAnonymous]
	public Task<IActionResult> GetFileMetadataAsync([FromRoute] Guid id)
	{
		var query = new GetDocumentMetadataQuery(id);

		return GetDocumentMetadataResponseAsync(query);
	}

    [HttpGet("{id:guid}/download")]
	[AllowAnonymous]
    public async Task<IResult> GetFileAsync([FromRoute] Guid id)
    {
		var userIsInAdminRole = HttpContext.User.IsInRole("Admin");
        var query = new DownloadDocumentQuery(id, isAdminRequest: userIsInAdminRole);

        var result = await _mediator.Send(query);

        if (!result)
            return Results.NotFound();

        var file = result.Value;
		return Results.Stream(file.ContentStream, file.ContentType, file.Name);
    }

	[HttpGet("")]
	[AllowAnonymous]
	public Task<IActionResult> GetPublicFilesAsync()
	{
		var query = new GetDocumentMetadataQuery();

		return GetDocumentMetadataResponseAsync(query);
    }

	[HttpGet("/admin/documents")]
	public Task<IActionResult> GetAllFilesAsync()
	{
		var query = new GetDocumentMetadataQuery(adminRequest: true);

		return GetDocumentMetadataResponseAsync(query, adminRequest: true);
    }

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteDocumentAsync([FromRoute] Guid id)
	{
		var command = new DeleteDocumentCommand(id);

		var response = await _mediator.Send(command);

		return StatusCode((int)response.Code, response);
	}

	[HttpPatch("{id:guid}")]
	public async Task<IActionResult> EditDocumentAnnotationOrPublicityAsync([FromRoute] Guid id, [FromBody] EditDocumentInfoDto editDocumentInfoDto)
	{
		var command = new UpdateDocumentMetadataCommand(id, editDocumentInfoDto.IsPublic, editDocumentInfoDto.Annotation);

		var response = await _mediator.Send(command);

		if (response)
			return NoContent();

		return StatusCode((int)response.Code, response);
	}

	private async Task<IActionResult> GetDocumentMetadataResponseAsync(GetDocumentMetadataQuery query, bool adminRequest = false)
	{
		var response = await _mediator.Send(query);

		if (adminRequest && response.IsSuccess)
			return StatusCode((int)response.Code, new HttpResponse<IEnumerable<AdminDocumentMetadataDto>>(response.Value.Cast<AdminDocumentMetadataDto>()));

		return StatusCode((int)response.Code, response);
	}
}
