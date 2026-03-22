using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Furijat.Services.Jwt.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Furijat.Services.Jwt;

public class JWT : IJWT
{
    private readonly string _audienceUrl;
    private readonly IConfiguration _config;
    private readonly string _secretKey;

    public JWT(IConfiguration config)
    {
        _config = config;
        _secretKey = _config["SecretKey"] ?? throw new InvalidOperationException();
        _audienceUrl = _config["ClientUrl"] ?? throw new InvalidOperationException();
    }

    public string CreateToken(JWTRequestDTO jwtRequest)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("user_id", jwtRequest.UserId), new Claim(ClaimTypes.Role, jwtRequest.UserType.ToString())
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