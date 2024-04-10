using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Seedium.Repositories.Interface;

namespace Seedium.Repositories.Implementation;

public class JwtTokenRepository : IJwtTokenRepository
{
    private readonly IConfiguration _config;

    public JwtTokenRepository(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateJwtToken(IdentityUser user, List<string> roles)
    {
        var claims = new List<Claim> { new(ClaimTypes.Email, user.Email!), };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
