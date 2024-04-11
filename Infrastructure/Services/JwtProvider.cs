using Core.Interfaces.Services;
using Core.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtSettings;

    public JwtProvider(IOptions<JwtOptions> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string Generate(IEnumerable<string> roles)
    {
        var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.Sub, "1"),
               new Claim(JwtRegisteredClaimNames.Email, "prueba@prueba.com"),

           };
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                null,
                DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signInCredentials
            );

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
   
}