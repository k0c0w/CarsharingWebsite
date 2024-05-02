using Shared;

namespace GraphQL.API.Helpers.Extensions;

public static class IHttpContextAccessorExtensions
{
    public static string GetUserId(this IHttpContextAccessor httpContextAccessor) 
        => httpContextAccessor.HttpContext!.User.GetId();
}
