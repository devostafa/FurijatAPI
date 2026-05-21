using Furijat.Services.JWT.DTO;

namespace Furijat.Services.JWT;

public interface IJWTService
{
    public string CreateToken(JWTRequestDTO jwtRequest);
}