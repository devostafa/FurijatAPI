using Furijat.Services.Jwt.DTO;

namespace Furijat.Services.Jwt;

public interface IJWT
{
    public string CreateToken(JWTRequestDTO jwtRequest);
}