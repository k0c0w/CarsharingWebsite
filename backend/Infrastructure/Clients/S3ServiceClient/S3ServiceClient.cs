using Clients.Objects;
using Clients.S3ServiceClient.ResponseModels;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace Clients.S3ServiceClient
{
    public class S3ServiceClient : ClientBase
    {
        public S3ServiceClient(string baseUrl, HttpClient httpClient) : base(baseUrl, httpClient)
        {
        }

        public async Task<WebCallResult<UploadFileResult>> CreateFileAsync(string fileName, string bucketName, Stream bytes, string contentType)
        {
            if (!ValidContentType(contentType))
                return new WebCallResult<UploadFileResult>(new ArgumentError<UploadFileResult>($"Unsupported Content-Type: {contentType}"));

            var requestPath = $"files/{bucketName}";
            var requestContent = new MultipartFormDataContent();
            {
                var streamContent = new StreamContent(bytes);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                requestContent.Add(streamContent, "file", fileName);
            }

            var request = CreateRequestMessage(HttpMethod.Post, requestPath, requestContent);

            try
            {
                var response = await HttpClient.SendAsync(request);
                var responseText = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new WebCallResult<UploadFileResult>(new UploadFileResult() { Success = true, Url = $"{BaseUri}/{requestPath}/{fileName}"});
                }

                var json = JsonNode.Parse(responseText);

                int? code = default;
                if (!int.TryParse(json["code"]?.ToString(), out int responseCode))
                    code = responseCode;

                return new WebCallResult<UploadFileResult>(new ServerError<UploadFileResult>(code, json["errorMessage"].ToString()));
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
