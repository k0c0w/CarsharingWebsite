using Microsoft.AspNetCore.Authorization;

namespace Carsharing.Helpers.Attributes
{
    public class MessageAuthorOrAdminOnlyAttribute : AuthorizeAttribute
    {
    }
}
