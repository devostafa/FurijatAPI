using Furijat.Data.Enums;

namespace Furijat.Data.DTOs.ResponseDTO;

public record ProjectResponseDTO(
    Guid Id,
    string Title,
    string Description,
    ProjectCategoryEnum Category,
    Guid UserId,
    UserResponseDTO UserOwner,
    string Facebook,
    string? X,
    string? Instagram,
    int? CurrentFund,
    int? TotalFundRequired,
    string[]? ImagesNames
    );