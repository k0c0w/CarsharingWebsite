using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Carsharing.Helpers.Options;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Carsharing.Helpers.Authorization;

public class JwtGenerator : IJwtGenerator
{
    private readonly SymmetricSecurityKey _key;

    public JwtGenerator(IOptions<JwtOptions> jwtOptions)
    {
        Console.WriteLine($"AAAAAAAAAAAAAAAAA: {jwtOptions.Value.Key}");
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
    }

    public string CreateToken(User user, IEnumerable<Claim> claims)
    {
       

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}