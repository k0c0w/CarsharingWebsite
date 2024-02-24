using MediatR;
using MinioConsumer.Services;
using MinioConsumers.Services;
using Shared.Results;

namespace MinioConsumer.Features.AbstractFile.Query;

public class GetAbstractFileQueryHandler : IRequestHandler<GetAbstractFileQuery, Result<S3File>>
{
    private readonly IS3Service _s3Service;


    public GetAbstractFileQueryHandler(IS3Service s3Service)
    {
        _s3Service = s3Service;
    }


    async Task<Result<S3File>> IRequestHandler<GetAbstractFileQuery, Result<S3File>>.Handle(GetAbstractFileQuery request, CancellationToken cancellationToken)
    {
        var file = await _s3Service.GetFileFromBucketAsync(request.BucketName, request.FileName);

        if (file == null)
        {
            return new Error<S3File>();
        }

        return new Ok<S3File>(file);
    }
}
