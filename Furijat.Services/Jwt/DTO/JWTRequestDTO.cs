using Furijat.Data.Enums;

namespace Furijat.Services.Jwt.DTO;

public class JWTRequestDTO
{
    public string UserId { get; set; }
    public UserTypeEnum UserType { get; set; }
}