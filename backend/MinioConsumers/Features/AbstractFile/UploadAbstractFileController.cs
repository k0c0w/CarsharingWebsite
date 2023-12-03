using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinioConsumer.Features.AbstractFile.Query;
using MinioConsumer.Services;

namespace MinioConsumer.Features.UploadAbstractFile;

[Route("files")]
//[Authorize]
public class UploadAbstractFileController : ControllerBase
{
    private readonly ISender _sender;

    public UploadAbstractFileController(ISender sender)
    {
        _sender = sender;
    }


    [HttpPost("{bucketName}")]
    public async Task<IActionResult> UploadFileAsync([FromRoute] string bucketName, IFormFile file)
    {
        using var stream = file?.OpenReadStream();
        var fileToSave = new S3File(file?.FileName, bucketName, stream, file?.ContentType);
        var response = await _sender.Send(new UploadAbstractFileCommand(fileToSave));

        if (response.IsSuccess)
            return Created(response!.Message!, new
            {
                Url = response.Message
            });

        return StatusCode((int)response.Code, response);
    }

    [HttpGet("{bucketName}/{fileName}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFileAsync([FromRoute] string bucketName, [FromRoute] string fileName)
    {
        var response = await _sender.Send(new GetAbstractFileQuery(bucketName, fileName));

        if (!response)
            return NotFound();

        var file = response.Value;
        return new FileStreamResult(file!.ContentStream, file.ContentType);
    }
}
