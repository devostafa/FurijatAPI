using Furijat.Data.Data.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Furijat.Services.Services.Mail;

public class Mail : IMail
{
    private readonly IConfiguration _config;
    private IConfigurationSection _emailsettings;

    public Mail(IConfiguration config)
    {
        _config = config;
        _emailsettings =_config.GetSection("EmailSettings");
    }
    
    public async Task<bool> SendMail(MailRequest mailRequest)
    {
        var emailmessage = new MimeMessage();
        emailmessage.From.Add(new MailboxAddress(_emailsettings["SenderName"], _emailsettings["SenderEmail"]));
        emailmessage.To.Add(new MailboxAddress("Test", mailRequest.Emailto));
        emailmessage.Subject = mailRequest.Subject;
        emailmessage.Body = new TextPart("plain") { Text = mailRequest.Message };

        var client = new SmtpClient();
        await client.ConnectAsync(_emailsettings["SmtpServer"], Int16.Parse(_emailsettings["Port"]), SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_emailsettings["CompanyMail"], _emailsettings["CompanyPassword"]);
        await client.SendAsync(emailmessage);
        return true;
        /*
                 bool check = _client.SendAsync(emailmessage).IsCompletedSuccessfully;
        if (check)
        {
            await _client.DisconnectAsync(true);
            return true;
        }
        else
        {
            return false;
        }
         */
    }
    
    public async Task MailNotifyDonator(string donatorEmail, Project project ,Donation donation)
    {
        string messagebody = $"Dear {project.User.Username}, your donation of amount {donation.DonationAmount} to project  {project.Title} has been registered and awaiting confirmation";
        MailRequest donatorMailNotify = new MailRequest {Emailto = donatorEmail, Subject = "Donation request registered", Message = messagebody};
        await SendMail(donatorMailNotify);
    }
    
    public async Task MailNotifyProjectOwner(string projectowneremail, Project project ,Donation donation)
    {
        string messagebody = $"Dear {project.User.Username}, your project {project.Title} has just received a donation of amount {donation.DonationAmount}";
        MailRequest projectownermailnotify = new MailRequest {Emailto = projectowneremail, Subject = "Received Donation", Message = messagebody};
        await SendMail(projectownermailnotify);
    }
    
    public async Task MailNotifyApproveDonator(Donation donation,User donator ,Project project)
    {
        string messagebody = $"Dear {donator.Username}, you have just donated amount of {donation.DonationAmount} to project {project.Title}, Thank You for Supporting your community";
        MailRequest donatornotifymail = new MailRequest {Emailto = donator.Email , Subject = "Donation Accepted", Message = messagebody};
        await SendMail(donatornotifymail);
    }
    
    public async Task MailNotifyRejectDonator(Donation donation,User donator ,Project project)
    {
        string messagebody = $"Dear {donator.Username}, your donation of {donation.DonationAmount} to project {project.Title}, has been rejected. Please try again at different time";
        MailRequest donatornotifymail = new MailRequest {Emailto = donator.Email , Subject = "Donation Rejected", Message = messagebody};
        await SendMail(donatornotifymail);
    }
    
    
}