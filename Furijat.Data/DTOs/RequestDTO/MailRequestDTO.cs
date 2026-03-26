using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Enums;

namespace Furijat.Data.DTOs.RequestDTO;

public class MailRequestDTO
{
    public MailRequestTypeEnum MailType { get; set; }
    public string Emailto { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }

    public UserDTO? User { get; set; }

    public ProjectResponseDTO? Project { get; set; }

    public DonationResponseDTO? Donation { get; set; }
}