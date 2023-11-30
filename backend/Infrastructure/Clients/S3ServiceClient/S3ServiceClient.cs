using Clients.Objects;
using Clients.S3ServiceClient.ResponseModels;
using System.Text.Json.Nodes;

namespace Clients.S3ServiceClient
{
    public class S3ServiceClient : ClientBase
    {
        public S3ServiceClient(HttpClient httpClient) : base("https://localhost:8000", httpClient)
        {
        }

        public async Task<WebCallResult<UploadFileResult>> CreateFileAsync(string bucketName, string fileName, Stream bytes, string contentType)
        {
            if (!ValidContentType(contentType))
                return new WebCallResult<UploadFileResult>(new ArgumentError<UploadFileResult>($"Unsupported Content-Type: {contentType}"));

            var requestPath = $"files/{bucketName}/{fileName}";
            var request = CreateRequestMessage(HttpMethod.Post, $"files/{bucketName}/{fileName}");
            request.Content = new StreamContent(bytes);
            request.Headers.Add("Content-Type", contentType);

            try
            {
                var response = await HttpClient.SendAsync(request);
                var responseText = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new WebCallResult<UploadFileResult>(new UploadFileResult() { Success = true, Url = $"{BaseUri}/{requestPath}"});
                }

                var json = JsonNode.Parse(responseText);

                return new WebCallResult<UploadFileResult>(new UploadFileResult() { Message = json["message"].ToString() });
            }
            catch(HttpRequestException ex)
            {
                return new WebCallResult<UploadFileResult>(new CantConnectError(ex.Message, ex));
            }
        }

        private bool ValidContentType(string contentType)
        {
            return contentType switch
            {
                "image/png" => true,
                "image/jpeg" => true,
                _ => false,
            };
        }

        //todo: validate bucket and filename
    }
}
