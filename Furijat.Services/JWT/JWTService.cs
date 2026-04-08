using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Furijat.Services.Jwt.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Furijat.Services.Jwt;

public class JWTService : IJWTService
{
    private readonly string _audienceUrl;
    private readonly IConfiguration _config;
    private readonly string _secretKey;

    public JWTService(IConfiguration config)
    {
        _config = config;
        _secretKey = _config["SecretKey"] ?? throw new InvalidOperationException("SecretKey configuration value is missing.");
        _audienceUrl = _config["ClientUrl"] ?? throw new InvalidOperationException("ClientUrl configuration value is missing.");
    }

    public string CreateToken(JWTRequestDTO jwtRequest)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("userId", jwtRequest.UserId), new Claim(ClaimTypes.Role, jwtRequest.UserType.ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var data = new JwtSecurityToken(
            claims: claims,
            issuer: _config["Url"],
            audience: _audienceUrl,
            expires: DateTime.Now.AddDays(14),
            signingCredentials: creds
        );
        var token = new JwtSecurityTokenHandler().WriteToken(data);
        return token;
    }
}