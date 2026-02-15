namespace Furijat.Data.Data.Models;

public class Donation
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public decimal DonationAmount { get; set; }
    public DateOnly Date { get; set; }
    public string PaymentType { get; set; }
    public bool Status { get; set; } = false;
}