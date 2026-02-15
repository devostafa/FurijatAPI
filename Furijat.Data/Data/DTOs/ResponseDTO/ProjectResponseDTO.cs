using Furijat.Data.Data.Models;

namespace Furijat.Data.Data.DTOs.ResponseDTO;

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
    public string X { get; init; }
    public string Instagram { get; init; }
    public int Currentfund { get; init; }
    public int Totalfundrequired { get; init; }
    public string[] Imagesnames { get; init; }
    public List<Donation> Donations { get; init; }
}