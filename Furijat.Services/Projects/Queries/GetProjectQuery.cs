using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Services.Base.Queries;

namespace Furijat.Services.Projects.Queries;

public record GetProjectQuery(string Id) : IQuery<ProjectResponseDTO>;