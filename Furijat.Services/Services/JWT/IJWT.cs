using Furijat.Services.Services.JWT.DTO;

namespace Furijat.Services.Services.JWT;

public interface IJWT
{
    public string CreateToken(JWTRequestDTO jwtrequest);
}