namespace Furijat.Data.Data.DTOs.RequestDTO;

public record LoginRequestDTO
{
    public string Username { get; init; }
    public string Password { get; init; }
}