using Furijat.Services.Jwt.DTO;

namespace Furijat.Services.Jwt;

public interface IJWTService
{
    public string CreateToken(JWTRequestDTO jwtRequest);
}