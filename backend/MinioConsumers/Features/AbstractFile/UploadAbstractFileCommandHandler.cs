using MediatR;
using MinioConsumers.Services;
using Shared.CQRS;
using Shared.Results;

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
            // todo: check if it contains port
            Domain = request.Host.Value;
        }
    }

    async Task<HttpResponse> IRequestHandler<UploadAbstractFileCommand, HttpResponse>.Handle(UploadAbstractFileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var s3fFile = request.File;

            if(!await _s3Service.BucketExsistAsync(s3fFile.BucketName))
                await _s3Service.CreateBucketAsync(s3fFile.BucketName);

            await _s3Service.PutFileInBucketAsync(request.File);

            return new HttpResponse(message: $"{Domain}/{s3fFile.BucketName}/{s3fFile.Name}");
        }
        catch(Exception ex)
        {
            _exceptionLogger.LogError(ex.Message);
            return new HttpResponse(System.Net.HttpStatusCode.InternalServerError, error: "Error occured while saving file.");
        }
    }
}
