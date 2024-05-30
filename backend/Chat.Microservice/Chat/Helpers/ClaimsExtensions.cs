using System.Security.Claims;
using CommonExtensions.Claims;

namespace Chat.Helpers;

public static class ClaimsExtensions
{
    public const string MANAGER_ROLE = "Manager";

    public static bool IsAuthenticatedUser(this ClaimsPrincipal claimsPrincipal)
    => claimsPrincipal.Identity != null && claimsPrincipal.Identity.IsAuthenticated;

    public static bool HasManagerRole(this ClaimsPrincipal claimsPrincipal) 
        => claimsPrincipal.UserIsInRole(MANAGER_ROLE);
}
