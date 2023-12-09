using MediatR;
using MinioConsumer.Models;
using MinioConsumer.Services.Repositories;
using MinioConsumer.Services;

namespace MinioConsumer.Features.OccasionAttachment;

public class CreateOccasionAttachmentCommandHandler : IRequestHandler<CreateOccasionAttachmentCommand, HttpResponse<Guid>>
{
    private readonly MetadataSaver<OccasionAttachmentMetadata> _metadataSaver;
    private readonly OperationRepository _operations;

    public CreateOccasionAttachmentCommandHandler(MetadataSaver<OccasionAttachmentMetadata> metadataSaver, OperationRepository operations)
    {
        _metadataSaver = metadataSaver;
        _operations = operations;
    }

    public async Task<HttpResponse<Guid>> Handle(CreateOccasionAttachmentCommand request, CancellationToken cancellationToken)
    {
        var trackingId = await _operations.CreateNewOperationAsync(request.AttachmetByUser);

        var attachmentsCount = request.Attachments.Count();
        if (attachmentsCount > 5 || attachmentsCount < 1)
        {
            await _operations.UpdateOperationStatusAsync(trackingId, OperationStatus.Failed);
            return new HttpResponse<Guid>(System.Net.HttpStatusCode.BadRequest, trackingId, error: "Invalid attachments count.");
        }

        //_ = Task.Run(async () =>
        //{
        var metadata = new OccasionAttachmentMetadata(trackingId, request.AttachmetByUser, attachmentsCount)
        {
            CreationDateTimeUtc = DateTime.UtcNow,
            AccessUserList = new List<Guid> { request.OccasionUserId },
        };

        var metadataCreationResult = await _metadataSaver.UploadFileAsync(trackingId, metadata);

        if (!metadataCreationResult)
            return new HttpResponse<Guid>(System.Net.HttpStatusCode.BadRequest, trackingId, error: metadataCreationResult.ErrorMessage);

        foreach(var attachment in request.Attachments)
        {
            var attachmentResult = await _metadataSaver.AppendFileAsync(trackingId, attachment);
            if (!attachmentResult)
                return new HttpResponse<Guid>(System.Net.HttpStatusCode.BadRequest, trackingId, error: attachmentResult.ErrorMessage);
        }

        await _metadataSaver.CommitOperationAsync(trackingId);
        //});

        return new HttpResponse<Guid>(trackingId);
    }
}
