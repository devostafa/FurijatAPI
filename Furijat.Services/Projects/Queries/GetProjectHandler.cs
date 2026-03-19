using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Repositories.ProjectsRepository;
using Furijat.Services.Base.Queries;

namespace Furijat.Services.Projects.Queries;

public class GetProjectHandler : IQueryHandler<GetProjectQuery, ProjectResponseDTO>
{

    private readonly IProjectsRepository _projectRepository;

    public GetProjectHandler(IProjectsRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectResponseDTO> HandleAsync(GetProjectQuery request, CancellationToken cancellationToken = default)
    {
        var result = await _projectRepository.GetProjectAsync(request.Id);

        return result;
    }
}