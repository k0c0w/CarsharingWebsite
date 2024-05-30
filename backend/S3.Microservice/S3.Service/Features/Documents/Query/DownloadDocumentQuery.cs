using MediatR;
using MinioConsumer.Models;
using MinioConsumer.Services;
using MinioConsumer.Services.Repositories;
using MinioConsumers.Services;
using Results;

namespace MinioConsumer.Features.Documents.Query;

public class DownloadDocumentQuery : IRequest<Result<S3File>>
{
	public Guid Id { get; }
    public bool IsAdminRequest { get; }

    public DownloadDocumentQuery(Guid guid, bool isAdminRequest = false)
	{
		Id = guid;
		IsAdminRequest = isAdminRequest;
	}
}

public class DownloadDocumentQueryHandler : IRequestHandler<DownloadDocumentQuery, Result<S3File>>
{
	private readonly IMetadataRepository<DocumentMetadata> metadataRepository;
	private readonly IS3Service s3Service;

	public DownloadDocumentQueryHandler(IMetadataRepository<DocumentMetadata> metadataRepository, IS3Service s3Service)
	{
		this.metadataRepository = metadataRepository;
		this.s3Service= s3Service;
	}

    public async Task<Result<S3File>> Handle(DownloadDocumentQuery request, CancellationToken cancellationToken)
    {
		try
		{
			var metadata = await metadataRepository.GetByIdAsync(request.Id);
			if (metadata== null || !(request.IsAdminRequest || metadata.IsPublic))
				return new Error<S3File>();
			var fileinfo = metadata.LinkedFileInfos.First();
			var file = await s3Service.GetFileFromBucketAsync(fileinfo.TargetBucketName, fileinfo.ObjectName);

			return new Ok<S3File>(new S3File(fileinfo.OriginalFileName, file.BucketName, file.ContentStream, file.ContentType));
		}
		catch
		{
            return new Error<S3File>();
        }
    }
}
