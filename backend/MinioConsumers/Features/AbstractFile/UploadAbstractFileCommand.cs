using MediatR;
using MinioConsumer.Services;

namespace MinioConsumer.Features.UploadAbstractFile;

public class UploadAbstractFileCommand : IRequest<HttpResponse>
{
	public S3File File { get; }

	public UploadAbstractFileCommand(S3File file)
	{
		File = file;
	}
}
