using MediatR;
using MinioConsumer.Models;
using MinioConsumer.Services;
using MinioConsumer.Services.Repositories;

namespace MinioConsumer.Features.Documents;

public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, HttpResponse>
{
    private readonly MetadataSaver<DocumentMetadata> _metadataSaver;
    private readonly OperationRepository _operations;

    public CreateDocumentCommandHandler(MetadataSaver<DocumentMetadata> metadataSaver, OperationRepository operations)
    {
        _metadataSaver = metadataSaver;
        _operations = operations;
    }

    public async Task<HttpResponse> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var trackingId = await _operations.CreateNewOperationAsync(request.UserId);

        //_ = Task.Run(async () =>
        //{
            var metadata = new DocumentMetadata(trackingId, default)
            {
                IsPublic = !request.IsPrivate,
                CreationDateTimeUtc = DateTime.UtcNow,
                Annotation = request.AnnotationToFile,
            };
            if (request.File is null)
            {
                await _metadataSaver.UploadFileAsync(trackingId, metadata);
            }
            else
            {
                if (await _metadataSaver.UploadFileAsync(trackingId, metadata, request.File))
                    await _metadataSaver.CommitOperationAsync(trackingId);
            }
        //});

        return new HttpResponse(trackingId.ToString());
    }
}
