using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Enums;

namespace Furijat.Data.Repositories.ProjectsRepository;

public interface IProjectsRepository
{
    public Task<List<ProjectResponseDTO>> GetProjectsAsync(string? categoryId);

    public Task<ProjectResponseDTO> GetProjectAsync(string projectId);

    public Task<bool> AddProjectAsync(ProjectRequestDTO newProjectRequest);

    public Task<bool> UpdateProjectAsync(ProjectRequestDTO projectUpdateRequest);

    public Task<bool> UpdateProjectLikes(string projectId);

    public Task<bool> UpdateProjectStatusAsync(string projectId, ProjectStatusEnum statusUpdate);

    public Task<bool> RemoveProjectAsync(string projectId);

    public Task CreateFoldersAsync();
}