using System.Security.Claims;

namespace Carsharing;

public static class ClaimsExtensions
{
    public static string GetId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}