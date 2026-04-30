using Furijat.Data.DTOs.RequestDTO;
using Furijat.Services.Mail;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Route("mail")]
public class MailController(IMailService mailService) : BaseController
{
    [HttpPost("send")]
    public async Task<bool> SendMail(MailRequestDTO mailRequestDto)
    {
        return await mailService.SendMailAsync(mailRequestDto);
    }
}