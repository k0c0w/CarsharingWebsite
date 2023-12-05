using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinioConsumer.Features.Documents.InputModels;
using MinioConsumer.Features.Documents.Query;

namespace MinioConsumer.Features.Documents;

[Route("documents")]
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
		var creator = HttpContext.User.Identity.Name;
		var command = new CreateDocumentCommand(documentInfo.IsPrivate, Guid.NewGuid(), document);

		var response = await _mediator.Send(command);
		if (response)
			return Accepted(response);

		return StatusCode((int)response.Code, response);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetFileMetadataAsync([FromRoute] Guid id)
	{
		var query = new GetDocumentMetadataQuery(id);

		var result = await _mediator.Send(query);

		return StatusCode((int)result.Code, result);
	}

    [HttpGet("{id:guid}/download")]
    public async Task<IResult> GetFileAsync([FromRoute] Guid id)
    {
        var query = new DownloadDocumentQuery(id);

        var result = await _mediator.Send(query);

        if (!result)
            return Results.NotFound();

        var file = result.Value;
		return Results.Stream(file.ContentStream, file.ContentType, file.Name);
    }
}
