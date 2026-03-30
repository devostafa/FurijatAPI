using Furijat.Data.Enums;

namespace Furijat.Data.DTOs.ResponseDTO;

public record UserResponseDTO(
    Guid Id,
    string Name,
    UserTypeEnum UserType,
    string PhoneNumber,
    string Email,
    string Facebook,
    string X,
    string Instagram,
    ProjectResponseDTO Project
    );