using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.DTOs.ResponseDTO;

namespace Furijat.Data.Repositories.ProjectsRepository;

public interface IProjectsRepository
{
    public Task<List<ProjectResponseDTO>> GetProjectsAsync(string? categoryId);

    public Task<ProjectResponseDTO> GetProjectAsync(string projectId);

    public Task<bool> AddProjectAsync(ProjectRequestDTO requestDto);

    public Task<bool> UpdateProjectAsync(ProjectRequestDTO requestDto);

    public Task<bool> RemoveProjectAsync(string projectId);

    public Task CreateFoldersAsync();
}