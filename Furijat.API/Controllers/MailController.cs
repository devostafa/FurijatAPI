using Furijat.Data.Models;
using Furijat.Services.Mail;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Route("mail")]
public class MailController(IMail mailService) : BaseController
{
    [HttpPost("send")]
    public async Task<bool> SendMail(MailRequest mailRequest)
    {
        return await mailService.SendMail(mailRequest);
    }
}