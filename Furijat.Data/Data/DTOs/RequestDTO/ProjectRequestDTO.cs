using Microsoft.AspNetCore.Http;

namespace Furijat.Data.Data.DTOs.RequestDTO;

public record ProjectRequestDTO
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Subtitle { get; init; }
    public string Description { get; init; }
    public Guid CategoryId { get; init; }
    public Guid? UserId { get; init; }
    public int Totalfundrequired { get; init; }
    public string Facebook { get; init; }
    public string X { get; init; }
    public string Instagram { get; init; }
    public IFormFile[] ImagesFiles { get; init; }
    public bool IsAccepted { get; init; }
}