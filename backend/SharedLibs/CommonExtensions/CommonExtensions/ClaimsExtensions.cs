using System.Security.Claims;


namespace CommonExtensions.Claims;

public static class ClaimsExtensions
{
    public static string GetId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    }
    public static bool UserIsInRole(this ClaimsPrincipal claimsPrincipal, string role)
    {
        return claimsPrincipal.FindAll(ClaimTypes.Role).Any(x => x.Value == role);
    }
}