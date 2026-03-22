using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.Enums;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Utils;

namespace Furijat.Services.Mail;

public class Mail : IMail
{
    private readonly IConfiguration _config;
    private readonly IConfigurationSection _emailSettings;

    public Mail(IConfiguration config)
    {
        _config = config;
        _emailSettings = _config.GetSection("EmailSettings");
    }

    public async Task<bool> SendMailAsync(MailRequestDTO mailRequestDTO, MailRequestTypeEnum mailType)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings["SenderName"], _emailSettings["SenderEmail"]));
            message.To.Add(new MailboxAddress("", mailRequestDTO.Emailto));
            message.Subject = mailRequestDTO.Subject;

            var bannerImagePath = Path.Combine(_config["ContentRootPath"], "Storage", "Images", "mailBanner.png");
            var bannerImageAllBytes = File.ReadAllBytes(bannerImagePath);

            var mailBodyBuilder = new BodyBuilder();

            var bannerImage = mailBodyBuilder.LinkedResources.Add("banner.png", bannerImageAllBytes, ContentType.Parse("image/png"));

            bannerImage.ContentId = MimeUtils.GenerateMessageId();

            message.Body = new TextPart("html")
            {
                Text = $"""
                        <div style="text-align:center;">
                            <img src='cid:{bannerImage.ContentId}' alt='logo' width='100' height='100' style="display:inline-block;">
                            <h1>{message.Subject}</h1>
                            <p>{mailRequestDTO.Message}</p>
                        </div>
                        """
            };

            var client = new SmtpClient();
            await client.ConnectAsync(_emailSettings["SmtpServer"], short.Parse(_emailSettings["Port"]), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailSettings["CompanyMail"], _emailSettings["CompanyPassword"]);
            await client.SendAsync(message);

            await client.DisconnectAsync(true);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}