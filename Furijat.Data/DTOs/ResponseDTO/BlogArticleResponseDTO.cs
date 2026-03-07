namespace Furijat.Data.DTOs.ResponseDTO;

public record BlogArticleResponseDTO
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Subtitle { get; init; }
    public string Description { get; init; }
    public DateOnly PublishDate { get; init; }
}