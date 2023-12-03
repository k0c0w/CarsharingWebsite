using Shared.Results;
using System.Net;

namespace MinioConsumer.Features;

public record class HttpResponse : Result
{
    public HttpResponse(HttpStatusCode code, string? message = default, string? error = default) : base(code.IsSuccessful(), error)
    {
        Code = code;
        Message = message;
    }

    public HttpResponse(string? message = default) : this(HttpStatusCode.OK, message) { }

    public HttpStatusCode Code { get; } = HttpStatusCode.OK;

    public string? Message { get; }
}
