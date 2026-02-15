using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Furijat.Services.Services.JWT.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Furijat.Services.Services.JWT;

public class Jwt : IJWT
{
    private IConfiguration _config;
    private string apihost;
    private string jwtseckey;
    private string audienceUrl;
    private readonly IHttpContextAccessor _httpcontext;

    public Jwt(IConfiguration config, IHttpContextAccessor httpcontext)
    {
        _config = config;
        jwtseckey = _config["secretkey"]!;
        audienceUrl = _config["clientURL"]!;
        _httpcontext = httpcontext;
    }

    public string CreateToken(JWTRequestDTO jwtrequest)
    {
        
        //string baseUrl = $"{_httpcontext.HttpContext.Request.Scheme}://{_httpcontext.HttpContext.Request.Host}{_httpcontext.HttpContext.Request.PathBase}";
        
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, jwtrequest.username),
            new Claim("userid", jwtrequest.Id.ToString()),
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