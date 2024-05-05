using System.Security.Claims;
using Domain.Entities;

namespace GraphQL.API.Helpers.Authorization;

public interface IJwtGenerator
{
    public string CreateToken(User user, IEnumerable<Claim> claims);
}