using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Enums;

namespace Furijat.Data.DTOs.RequestDTO;

public record MailRequestDTO(
    MailRequestTypeEnum MailType,
    string EmailTo,
    string Subject,
    string? CustomMessage,
    UserResponseDTO? User,
    ProjectResponseDTO? Project,
    DonationResponseDTO? Donation);