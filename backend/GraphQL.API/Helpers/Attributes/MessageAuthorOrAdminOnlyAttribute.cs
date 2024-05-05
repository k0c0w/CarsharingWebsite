using Microsoft.AspNetCore.Authorization;

namespace GraphQL.API.Helpers.Attributes
{
    public class MessageAuthorOrAdminOnlyAttribute : AuthorizeAttribute
    {
    }
}
