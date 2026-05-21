using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Services.Base.Queries;

namespace Furijat.Services.Projects.Queries;

public record GetProjectsQuery(int? pageNumber, string? categoryId) : IQuery<PaginatedProjectsResponseDTO>;