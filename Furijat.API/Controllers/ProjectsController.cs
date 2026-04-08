using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Repositories.ProjectsRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Route("projects")]
public class ProjectsController : BaseController
{
    private readonly IProjectsRepository _projectsRepo;

    public ProjectsController(IProjectsRepository projectsRepo)
    {
        _projectsRepo = projectsRepo;
    }

    [HttpGet("projects/{pageNumber}")]
    public async Task<IActionResult> GetProjects(int pageNumber)
    {
        var pageSize = 10;

        if (pageNumber == 0)
        {
            List<ProjectResponseDTO> projects = await _projectsRepo.GetProjectsAsync();
            return Ok(projects);
        }
        else
        {
            List<ProjectResponseDTO> projects = await _projectsRepo.GetProjectsAsync();
            var totalPages = 0;
            var totalPagesDecimal = projects.Count / (decimal)pageSize;

            if (totalPagesDecimal % 1 == 0)
            {
                totalPages = (int)totalPagesDecimal;
            }
            else
            {
                totalPages = (int)totalPagesDecimal + 1;
            }

            List<ProjectResponseDTO> projectsRes = projects.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var response = new
            {
                totalPages, projects = projectsRes
            };
            return Ok(response);
        }
    }

    [HttpGet("project/{projectid}")]
    public async Task<ProjectResponseDTO> GetProject(string projectid)
    {
        return await _projectsRepo.GetProjectAsync(projectid);
    }

    [Authorize]
    [HttpPost("project/add")]
    public async Task<bool> AddProject(ProjectRequestDTO projecttoadd)
    {
        var authheader = HttpContext.Request.Headers["Authorization"];
        var token = "";

        if (authheader.ToString().StartsWith("Bearer"))
        {
            token = authheader.ToString().Substring("Bearer ".Length).Trim();
        }

        if (!string.IsNullOrEmpty(token))
        {
            return await _projectsRepo.AddProjectAsync(projecttoadd);
        }

        return false;
    }

    [Authorize]
    [HttpPost("project/update")]
    public async Task<bool> UpdateProject(ProjectRequestDTO projecttoadd)
    {
        return await _projectsRepo.UpdateProjectAsync(projecttoadd);
    }

    [Authorize]
    [HttpPost("project/remove")]
    public async Task<bool> RemoveProject(string projectid)
    {
        return await _projectsRepo.RemoveProjectAsync(projectid);
    }
}