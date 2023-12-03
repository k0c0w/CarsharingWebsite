using MassTransit;
using MinioConsumer.Services.Repositories;

namespace MinioConsumer.Services.PrimaryStorageSaver;

public class SaveInPRimaryDbRequestConsumer : IConsumer<SaveInPRimaryDbRequest>
{
    private OperationRepository _operationStatusRepository;
    private PrimaryStorageSaver _tempToPrimarySaver;

    public async Task Consume(ConsumeContext<SaveInPRimaryDbRequest> context)
    {
        var request = context.Message;

        _operationStatusRepository.UpdateOperationStatus(request.OperationId, Models.OperationStatus.InProggress);
        _ = Task.Run(() => _tempToPrimarySaver.MoveDataToPrimaryStorageAsync(request.OperationId, request.MetadataId));
    }
}
