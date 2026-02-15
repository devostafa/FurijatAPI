using Furijat.Data.Data.Models;

namespace Furijat.Services.Services.Mail;

public interface IMail
{
    public Task<bool> SendMail(MailRequest mailRequest);
    public Task MailNotifyDonator(string donatorEmail, Project project ,Donation donation);
    public Task MailNotifyProjectOwner(string projectowneremail, Project project ,Donation donation);
    public Task MailNotifyApproveDonator(Donation donation,User donator ,Project project);
    public Task MailNotifyRejectDonator(Donation donation,User donator ,Project project);
}