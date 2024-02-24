using System;
using System.Security.Claims;

namespace Shared;

public static class ClaimsExtensions
{
    public static string GetId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
    public static bool UserIsInRole(this ClaimsPrincipal claimsPrincipal, string role)
    {
        return claimsPrincipal.FindAll(ClaimTypes.Role).Any(x => x.Value == role);
    }
}