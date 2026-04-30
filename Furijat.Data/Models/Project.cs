using System.ComponentModel.DataAnnotations;
using Furijat.Data.Enums;

namespace Furijat.Data.Models;

public class Project
{
    [Key] public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Likes { get; set; }
    public ProjectStatusEnum Status { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int CurrentFund { get; set; }
    public int FundRequired { get; set; }

    public IList<string> ImagesNames { get; set; }

    // public IList<Donation> Donations { get; set; }
    public string? SocialMediaId { get; set; }
    public SocialMedia? SocialMedia { get; set; }
    public string? PaymentAccountId { get; set; }
    public PaymentAccount? PaymentAccount { get; set; }
}