namespace Furijat.Data.DTOs.ResponseDTO;

public record PaginatedProjectsResponseDTO(
    int TotalPages,
    IEnumerable<ProjectResponseDTO> Projects);