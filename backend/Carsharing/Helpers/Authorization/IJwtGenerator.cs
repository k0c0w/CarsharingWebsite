using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Carsharing.Helpers.Authorization;

public interface IJwtGenerator
{
    public string CreateToken(User user, IEnumerable<Claim> claims);
}