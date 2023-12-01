using MinioConsumer.Models;
using Shared.Results;

namespace MinioConsumer.Services;

public class MetadataSaver
{
    public Result UploadFile(MetadataBase metadata, IFormFile file) { return Result.SuccessResult; }

    public Result AppendFile(Guid operationId, IFormFile files) { return Result.SuccessResult; }

    public Result UploadFile(MetadataBase metadata) { return Result.SuccessResult; }

    private Result SaveMetadata(MetadataBase metadata)
    {
        //todo: save/update metadata by linked operation
        // use specific repository from abstractions
    }

    private Result SaveFile(Stream file)
    {

        //save content by operation id
    }

    public Result IsOperationCompleted(Guid id)
    {
        //todo: retrun if specific operation recieved all data
    }

    // call another service here
    public Result CommitOperation()
    {
        //todo: initilize saving to mongo and main s3 service
        // on complete it should update operation status
    }
}
