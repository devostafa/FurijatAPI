using Furijat.Data.Data.Models;

namespace Furijat.Data.Data.DTOs.ResponseDTO;

public record DonationResponseDTO
{
    public Guid Id { get; init; }
    public Guid ProjectId { get; init; }
    public Project Project { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
    public decimal Donationamount { get; init; }
    public DateOnly Date { get; init; }
    public bool Status { get; init; }
}