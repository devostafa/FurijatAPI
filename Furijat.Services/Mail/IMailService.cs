using Furijat.Data.DTOs.RequestDTO;

namespace Furijat.Services.Mail;

public interface IMailService
{
    public Task<bool> SendMailAsync(MailRequestDTO mailRequestDTO);
}