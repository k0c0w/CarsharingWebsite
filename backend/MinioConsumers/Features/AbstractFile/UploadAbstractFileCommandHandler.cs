using MediatR;
using MinioConsumer.Features.UploadAbstractFile;
using MinioConsumers.Services;

namespace MinioConsumer.Features.AbstractFile;

public class UploadAbstractFileCommandHandler : IRequestHandler<UploadAbstractFileCommand, HttpResponse>
{
    private readonly IS3Service _s3Service;
    private readonly ILogger<UploadAbstractFileCommandHandler> _logger;

    public UploadAbstractFileCommandHandler(IS3Service service, ILogger<UploadAbstractFileCommandHandler> logger)
    {
        _s3Service = service;
        _logger = logger;
    }

    public async Task<HttpResponse> Handle(UploadAbstractFileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var file = request.File;
            if (!await _s3Service.BucketExsistAsync(file.BucketName))
                await _s3Service.CreateBucketAsync(file.BucketName);
            await _s3Service.PutFileInBucketAsync(file);

            return new HttpResponse(code: System.Net.HttpStatusCode.OK);

        }
        catch(Exception ex) 
        {
            _logger.LogError("Erroe while creating file {exception}", ex);

            return new HttpResponse(code: System.Net.HttpStatusCode.BadRequest, message:"Ошибка при создании файла", error: ex.Message);
        }
    }
}

