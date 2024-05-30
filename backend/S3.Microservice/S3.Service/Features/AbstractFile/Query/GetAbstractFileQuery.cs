using MediatR;
using MinioConsumer.Services;
using Results;

namespace MinioConsumer.Features.AbstractFile.Query;

public class GetAbstractFileQuery : IRequest<Result<S3File>>
{
	public string BucketName { get; }

	public string FileName { get; }


	public GetAbstractFileQuery(string bucketName, string fileName)
	{
		BucketName = bucketName;
		FileName = fileName;
	}
}
