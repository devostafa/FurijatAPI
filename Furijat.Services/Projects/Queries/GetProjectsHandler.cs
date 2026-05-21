using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Repositories.ProjectsRepository;
using Furijat.Services.Base.Queries;

namespace Furijat.Services.Projects.Queries;

public class GetProjectsHandler : IQueryHandler<GetProjectsQuery, PaginatedProjectsResponseDTO>
{

    private readonly IProjectsRepository _projectRepository;

    public GetProjectsHandler(IProjectsRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<PaginatedProjectsResponseDTO> HandleAsync(GetProjectsQuery request, CancellationToken cancellationToken = default)
    {
        return await _projectRepository.GetProjectsAsync(request.pageNumber, request.category);
    }
}