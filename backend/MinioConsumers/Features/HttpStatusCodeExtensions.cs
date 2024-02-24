using System.Net;

namespace MinioConsumer.Features;

public static class HttpStatusCodeExtensions
{
    public static bool IsSuccessful(this HttpStatusCode code) => (int)code is >= 200 and < 300;

}