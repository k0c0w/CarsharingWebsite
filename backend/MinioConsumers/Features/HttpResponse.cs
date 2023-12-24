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

public record class HttpResponse<T> : Result<T>
{
    public HttpResponse(HttpStatusCode code, T? data, string? error = default) : base(code.IsSuccessful(), data, error)
    {
        Code = code;
    }

    public HttpResponse(T data) : this(HttpStatusCode.OK, data) { }

    public HttpStatusCode Code { get; } = HttpStatusCode.OK;
}
