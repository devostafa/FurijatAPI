namespace Furijat.Data.Data.DTOs.ResponseDTO;

public record CategoryResponseDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}