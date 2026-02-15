using Furijat.Data.Data.DTOs.RequestDTO;
using Furijat.Data.Data.DTOs.ResponseDTO;
using Furijat.Services.Services.Repositories.ProjectsRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;


[Route("Projects")]
public class ProjectsController : BaseController
{
    private readonly IProjectsRepository _projectsservice;

    public ProjectsController(IProjectsRepository projectsservice)
    {
        _projectsservice = projectsservice;
    }
    
    [HttpGet("GetProjects/{pagenumber}")]
    public async Task<IActionResult> GetProjects(int pagenumber)
    {
        int pageSize = 10;
        if (pagenumber == 0)
        {
            List<ProjectResponseDTO> projects = await _projectsservice.GetProjects();
            return Ok(projects);
        }
        else
        {
            var projects = await _projectsservice.GetProjects();
            int totalPages = 0;
            decimal totalPagesDecimal = (decimal)projects.Count / (decimal)pageSize;
            if (totalPagesDecimal % 1 == 0)
            {
                totalPages = (int)totalPagesDecimal;
            }
            else
            {
                totalPages = (int)totalPagesDecimal + 1;
            }
            var projectsRes = projects.Skip((pagenumber - 1) * pageSize).Take(pageSize).ToList();
            var response = new
            {
                totalPages,
                projects = projectsRes
            };
            return Ok(response);
        }
    }
    
    [HttpGet("GetProject/{projectid}")]
    public async Task<ProjectResponseDTO> GetProject(string projectid)
    { 
        return await _projectsservice.GetProject(projectid);
    }
    
    [Authorize]
    [HttpPost("AddProject")]
    public async Task<bool> AddProject(ProjectRequestDTO projecttoadd)
    {
        var authheader = HttpContext.Request.Headers["Authorization"];
        string token = "";
        if (authheader.ToString().StartsWith("Bearer"))
        {
            token = authheader.ToString().Substring("Bearer ".Length).Trim();
        }
        if (!string.IsNullOrEmpty(token))
        {
            return await _projectsservice.AddProject(projecttoadd);
        }
        else
        {
            return false;
        }
    }
    
    [Authorize]
    [HttpPost("UpdateProject")]
    public async Task<bool> UpdateProject(ProjectRequestDTO projecttoadd)
    {
        return await _projectsservice.UpdateProject(projecttoadd);
    }
    
    [Authorize]
    [HttpPost("RemoveProject")]
    public async Task<bool> RemoveProject(string projectid)
    {
        return await _projectsservice.RemoveProject(projectid);
    }
    
    
    
}