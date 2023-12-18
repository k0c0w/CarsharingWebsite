using Clients.Objects;
using Clients.S3ServiceClient.ResponseModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Clients.S3ServiceClient
{
    public class S3ServiceClient : ClientBase
    {
        public S3ServiceClient(string baseUrl, HttpClient httpClient) : base(baseUrl, httpClient)
        {
        }

        public async Task<WebCallResult<IEnumerable<AttachmentInfo>>> GetAttachmentInfosByIdsAsync(Guid attachmentGuid)
        {
            if (attachmentGuid == default)
                return new WebCallResult<IEnumerable<AttachmentInfo>>(new ArgumentError<IEnumerable<AttachmentInfo>> ("invalid id"));

            try
            {
                var requestPath = $"attachments/{attachmentGuid}";
                var request = CreateRequestMessage(HttpMethod.Get, requestPath);
                var response = await HttpClient.SendAsync(request);
                var responseModel = await response.Content.ReadFromJsonAsync<HttpResponse<OccasionAttachmentInfo>>();
                if (!response.IsSuccessStatusCode)
                {
                    return new WebCallResult<IEnumerable<AttachmentInfo>>(new ServerError<IEnumerable<AttachmentInfo>>(responseModel.ErrorMessage));
                }

                return new WebCallResult<IEnumerable<AttachmentInfo>>(responseModel.Value.Attachments);
            }
            catch (HttpRequestException ex)
            {
                return new WebCallResult<IEnumerable<AttachmentInfo>>(new CantConnectError(ex.Message, ex));
            }
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
                    return new WebCallResult<UploadFileResult>(new UploadFileResult() { Success = true, Url = $"{BaseUri}/{requestPath}/{fileName}" });
                }

                var json = JsonNode.Parse(responseText);

                int? code = default;
                if (!int.TryParse(json["code"]?.ToString(), out int responseCode))
                    code = responseCode;

                return new WebCallResult<UploadFileResult>(new ServerError<UploadFileResult>(code, json["errorMessage"].ToString()));
            }
            catch (HttpRequestException ex)
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

public record class HttpResponse<T>
{
    public T? Value { get; set; }

    public string ErrorMessage { get; set; }
}

public class OccasionAttachmentInfo
{
    [JsonPropertyName("uploaded_at")]
    public DateTime CreationDateUtc { get; set; }

    [JsonPropertyName("uploader")]
    public Guid UploaderId { get; set; }

    [JsonPropertyName("attachments")]
    public IEnumerable<AttachmentInfo> Attachments { get; set; } = Array.Empty<AttachmentInfo>();
}

public class AttachmentInfo
{
    [JsonPropertyName("content_type")]
    public string ContentType { get; set; }

    [JsonPropertyName("download_url")]
    public string DownloadUrl { get; set; }

    [JsonPropertyName("file_name")]
    public string FileName { get; set; }
}