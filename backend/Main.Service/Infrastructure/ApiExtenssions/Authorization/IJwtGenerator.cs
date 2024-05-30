using System.Security.Claims;
using Domain.Entities;

namespace ApiExtensions.Authorization;

public interface IJwtGenerator
{
    public string CreateToken(User user, IEnumerable<Claim> claims);
}