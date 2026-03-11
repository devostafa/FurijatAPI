using Furijat.Data.Enums;
using Furijat.Data.Repositories.ProjectsRepository;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

public class AdminController : BaseController
{
    private readonly IProjectsRepository _projectRepo;

    public AdminController(IProjectsRepository projectRepo)
    {
        _projectRepo = projectRepo;
    }
    
    [HttpGet("project/approve/{projectId}")]
    public async Task<IActionResult> ApproveProject(string projectId)
    {
        var check = await _projectRepo.UpdateProjectStatus(projectId, ProjectStatusEnum.Approved);
        
        return Ok(check);
    }
    
    [HttpGet("project/reject/{projectId}")]
    public async Task<IActionResult> RejectProject(string projectId)
    {
        var check = await _projectRepo.UpdateProjectStatus(projectId, ProjectStatusEnum.Rejected);
        
        return Ok(check);
    }
}