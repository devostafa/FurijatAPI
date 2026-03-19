using Furijat.Data.Models;

namespace Furijat.Data.DTOs.ResponseDTO;

public record ProjectResponseDTO
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Subtitle { get; init; }
    public string Description { get; init; }
    public Guid CategoryId { get; init; }
    public Category Category { get; init; }
    public Guid UserId { get; init; }
    public UserDTO User { get; init; }
    public string Facebook { get; init; }
    public string? X { get; init; }
    public string? Instagram { get; init; }
    public int? CurrentFund { get; init; }
    public int? TotalFund { get; init; }
    public string[]? Images { get; init; }
    public List<DonationResponseDTO>? Donations { get; init; }
}