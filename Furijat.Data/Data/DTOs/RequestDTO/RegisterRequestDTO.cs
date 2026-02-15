namespace Furijat.Data.Data.DTOs.RequestDTO;

public record RegisterRequestDTO
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}