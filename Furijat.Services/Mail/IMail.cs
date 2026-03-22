using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.Enums;

namespace Furijat.Services.Mail;

public interface IMail
{
    public Task<bool> SendMailAsync(MailRequestDTO mailRequestDTO, MailRequestTypeEnum mailType);
}