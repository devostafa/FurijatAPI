using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.Models;

namespace Furijat.Data.Repositories.ProjectsRepository;

public interface IProjectsRepository
{
    public Task<List<Project>> GetProjects();

    //public Task<List<Project>> GetProjectsOfCategory(string categoryid);
    public Task<Project> GetProject(string projectId);

    public Task<bool> AddProject(ProjectRequestDTO requestDto);

    public Task<bool> UpdateProject(ProjectRequestDTO requestDto);

    public Task<bool> RemoveProject(string projectId);

    public Task CreateFolders();
}