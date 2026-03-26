using Furijat.Data.DTOs.RequestDTO;

namespace Furijat.Services.Mail;

public interface IMail
{
    public Task<bool> SendMailAsync(MailRequestDTO mailRequestDTO);
}