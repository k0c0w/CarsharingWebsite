using MassTransit;
using MinioConsumer.Models;
using MinioConsumer.Services.Repositories;

namespace MinioConsumer.Services.PrimaryStorageSaver;

public class SaveInPRimaryDbRequestConsumer<TMetadata> : IConsumer<SaveInPRimaryDbRequest<TMetadata>> where TMetadata : MetadataBase
{
    private OperationRepository _operationStatusRepository;
    private PrimaryStorageSaver<TMetadata> _tempToPrimarySaver;

    public async Task Consume(ConsumeContext<SaveInPRimaryDbRequest<TMetadata>> context)
    {
        var request = context.Message;

        await _operationStatusRepository.UpdateOperationStatusAsync(request.OperationId, Models.OperationStatus.InProggress);
        _ = Task.Run(() => _tempToPrimarySaver.MoveDataToPrimaryStorageAsync(request.OperationId, request.MetadataId));
    }
}
