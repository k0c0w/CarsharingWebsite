using System.Security.Claims;

namespace MinioConsumer
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (Guid.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out Guid id))
                return id;

            return default(Guid?);
        }

        public static bool UserIsInRole(this ClaimsPrincipal claimsPrincipal, string role)
        {
            return claimsPrincipal.FindAll(ClaimTypes.Role).Any(x => x.Value == role);
        }
    }
}
