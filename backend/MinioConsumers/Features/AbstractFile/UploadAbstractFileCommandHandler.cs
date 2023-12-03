using MediatR;
using MinioConsumers.Services;

namespace MinioConsumer.Features.UploadAbstractFile;

public class UploadAbstractFileCommandHandler : IRequestHandler <UploadAbstractFileCommand, HttpResponse>
{
    private readonly IS3Service _s3Service;
    private readonly ILogger<Exception> _exceptionLogger;

    private string Domain { get; } = string.Empty;

    public UploadAbstractFileCommandHandler(IS3Service service, ILogger<Exception> logger, IHttpContextAccessor contextAccessor)
    {
        _s3Service = service;
        _exceptionLogger = logger;

        var request = contextAccessor.HttpContext?.Request;
        if (request is not null)
        {
            Domain = request.Host.Value;
        }
    }

    async Task<HttpResponse> IRequestHandler<UploadAbstractFileCommand, HttpResponse>.Handle(UploadAbstractFileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var s3fFile = request.File;
            if (s3fFile.ContentStream is null || s3fFile.Name is null)
                return new HttpResponse(System.Net.HttpStatusCode.BadRequest, error: "No content recieved.");

            if(!await _s3Service.BucketExsistAsync(s3fFile.BucketName))
                await _s3Service.CreateBucketAsync(s3fFile.BucketName);

            await _s3Service.PutFileInBucketAsync(request.File);
            return new HttpResponse(message: $"{Domain}/files/{s3fFile.BucketName}/{s3fFile.Name}");
        }
        catch(Exception ex)
        {
            _exceptionLogger.LogError(ex.Message);
            return new HttpResponse(System.Net.HttpStatusCode.InternalServerError, error: "Error occured while saving file.");
        }
    }
}
