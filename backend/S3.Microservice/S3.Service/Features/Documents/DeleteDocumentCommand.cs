using MediatR;

namespace MinioConsumer.Features.Documents;

public class DeleteDocumentCommand : IRequest<HttpResponse>
{
    public Guid Id { get; }

	public DeleteDocumentCommand(Guid id)
	{
		Id = id;
	}
}
