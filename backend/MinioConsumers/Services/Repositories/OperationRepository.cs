using MinioConsumer.Models;
using System.Diagnostics;
using FileInfo = MinioConsumer.Models.FileInfo;

namespace MinioConsumer.Services.Repositories;


// will be used to track operations statuses
public class OperationRepository
{
    public Guid CreateNewOperation(Guid initializerUserId)
    {
        var operation = new OperationInfo
        {
            Id = Guid.NewGuid(),
            InitializerUserId = initializerUserId,
            OperationStatus = OperationStatus.Receiving,
        };

        // save operation 

        return operation.Id;
    }
    
    public bool IsOperationInitializedByUser(Guid operationId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public OperationInfo GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void UpdateOperationStatus(Guid operationId, OperationStatus status)
    {
        // can not downgrade status
        Debug.Assert(status != OperationStatus.Receiving);
    }

    public OperationStatus? GetOperationStatus(Guid guid)
    {
        /// find operation by id and return it
        throw new NotImplementedException();
    }
}


// inforamation to store in temp db to track files and metadata recivied
// must be stored no longer than 1 hour after operation accessing
public class UploadInfo
{
    public Guid OperationId { get; init; }

    //metadata target schema
    public string Schema { get; init; }

    public int TargetFilesCount { get; init; }

    public int RecievedFiles { get; init; }

    public FileInfo? RecivedFile { get; init; } 

    public MetadataBase UploadMetadata { get; init; }

    public bool UploadFullyCompleted { get; init; }
}

// information about operation
// stored in redis, stores not longer than 1 hour 
public class OperationInfo
{
    public Guid Id { get; init; }

    public OperationStatus OperationStatus { get; init; }

    // maybe we dont need it
    //public DateTime InitilizedAt { get; init; }

    //id of user who initialized operation
    public Guid InitializerUserId { get; init; }
}