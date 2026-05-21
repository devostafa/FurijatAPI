using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Enums;
using Furijat.Services.Base.Queries;

namespace Furijat.Services.Projects.Queries;

public record GetProjectsQuery(int? pageNumber, ProjectCategoryEnum? category) : IQuery<PaginatedProjectsResponseDTO>;