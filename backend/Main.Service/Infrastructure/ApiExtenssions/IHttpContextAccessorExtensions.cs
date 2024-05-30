using CommonExtensions.Claims;
using Microsoft.AspNetCore.Http;

namespace ApiExtensions;

public static class IHttpContextAccessorExtensions
{
    public static string GetUserId(this IHttpContextAccessor httpContextAccessor)
        => httpContextAccessor.HttpContext!.User.GetId();
}