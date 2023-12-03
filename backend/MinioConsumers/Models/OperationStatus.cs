namespace MinioConsumer.Models;

public enum OperationStatus
{
    /// <summary>
    /// Waiting for all data upload to temp storages
    /// </summary>
    Receiving,

    /// <summary>
    /// Operation uploading were canceled by request
    /// </summary>
    Canceled,

    /// <summary>
    /// Data recived and available from temp storages, saving to main storages in progress
    /// </summary>
    InProggress,

    /// <summary>
    /// Data saving succeed
    /// </summary>
    Completed,

    /// <summary>
    /// An error occured on one of the steps, operation canceled by system
    /// </summary>
    Failed
}
