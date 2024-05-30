
namespace Clients.S3ServiceClient.ResponseModels
{
    public class UploadFileResult
    {
        public string? Message { get; set; }

        public string? Url { get; set; }

        public bool Success { get; set; }
    }
}
