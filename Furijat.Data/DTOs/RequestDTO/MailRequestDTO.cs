using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Enums;

namespace Furijat.Data.DTOs.RequestDTO;

public class MailRequestDTO(
    MailRequestTypeEnum MailType,
    string Emailto,
    string Subject,
    string? CustomMessage,
    UserResponseDTO? User,
    ProjectResponseDTO? Project,
    DonationResponseDTO? Donation);