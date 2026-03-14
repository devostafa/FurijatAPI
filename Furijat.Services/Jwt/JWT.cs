using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Furijat.Services.JWT.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Furijat.Services.Jwt;

public class JWT : IJWT
{
    private readonly IConfiguration _config;
    private readonly string audienceUrl;
    private readonly string jwtseckey;

    public JWT(IConfiguration config)
    {
        _config = config;
        jwtseckey = _config["secretkey"]!;
        audienceUrl = _config["clientURL"]!;
    }

    public string CreateToken(JWTRequestDTO jwtRequest)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, jwtRequest.username), new Claim("userid", jwtRequest.Id.ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtseckey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var tokendata = new JwtSecurityToken(
            claims: claims,
            issuer: _config["URL"],
            audience: audienceUrl,
            expires: DateTime.Now.AddDays(2),
            signingCredentials: cred
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(tokendata);
        return jwt;
    }
}