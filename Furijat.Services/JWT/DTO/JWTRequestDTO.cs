using Furijat.Data.Enums;

namespace Furijat.Services.JWT.DTO;

public record JWTRequestDTO(string UserId, UserTypeEnum UserType);