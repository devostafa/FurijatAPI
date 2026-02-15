namespace Furijat.Data.Data.DTOs.RequestDTO;

public record DonationRequestDTO
{
    public Guid UserId { get; init; }
    public Guid ProjectId { get; init; }
    public string PaymentType { get; set; }
    public decimal DonationAmount { get; init; }
}