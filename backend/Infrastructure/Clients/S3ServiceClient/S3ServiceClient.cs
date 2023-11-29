using Clients.Objects;
using System.Net.Http.Json;

namespace Clients.S3ServiceClient
{
    public class S3ServiceClient : ClientBase
    {
        public S3ServiceClient(HttpClient httpClient) : base("https://localhost:8000", httpClient)
        {
        }

        public async Task<WebCallResult<string>> CreateFileAsync(string bucketName, string fileName, Stream bytes, string contentType)
        {
            if (!ValidContentType(contentType))
                return new WebCallResult<string>(new ArgumentError($"Unsupported Content-Type: {contentType}"));

            var request = CreateRequestMessage(HttpMethod.Post, $"files/{bucketName}/{fileName}");
            request.Content = new StreamContent(bytes);
            request.Headers.Add("Content-Type", contentType);

            try
            {
                var response = await HttpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var accessLink = await response.Content.ReadAsStringAsync();
                    return new WebCallResult<string>(response.StatusCode, response.Headers, default, default, request.RequestUri.AbsolutePath, default, HttpMethod.Post,default, accessLink, default);
                }

                var error = await response.Content.ReadFromJsonAsync();

                return new WebCallResult<string>(response.StatusCode, response.Headers, default, default, request.RequestUri.AbsolutePath, default, HttpMethod.Post, default, default, 
                    new ServerError(error));
            }
            catch(HttpRequestException ex)
            {
                return new WebCallResult<string>(new CantConnectError());
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
