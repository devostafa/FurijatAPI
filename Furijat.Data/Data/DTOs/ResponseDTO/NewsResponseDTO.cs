namespace Furijat.Data.Data.DTOs.ResponseDTO;

public record NewsResponseDTO
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Subtitle { get; init; }
    public string Description { get; init; }
    public DateOnly Published { get; init; }
    public string Imagecovername { get; init; }
}