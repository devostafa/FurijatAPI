using Furijat.Services.JWT.DTO;

namespace Furijat.Services.JWT;

public interface IJWT
{
    public string CreateToken(JWTRequestDTO jwtrequest);
}