using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Enums;
using Furijat.Data.Models;

namespace Furijat.Data.DTOs;

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
