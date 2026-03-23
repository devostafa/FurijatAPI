using Furijat.Data.Enums;

namespace Furijat.Services.Jwt.DTO;

public record JWTRequestDTO(string UserId, UserTypeEnum UserType);