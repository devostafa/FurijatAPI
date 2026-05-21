using Furijat.Data.DTOs.RequestDTO;
using Furijat.Services.Base.Commands;
using Furijat.Services.Base.Queries;
using Furijat.Services.Projects.Commands;
using Furijat.Services.Projects.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Route("projects")]
public class ProjectsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) : BaseController
{
    [HttpGet("projects")]
    public async Task<IActionResult> GetProjects(int pageNumber, string? categoryId)
    {
        var query = new GetProjectsQuery(pageNumber, categoryId);

        var result = await queryDispatcher.QueryAsync(query);

        return Ok(result);
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetProject(string projectId)
    {
        var query = new GetProjectQuery(projectId);

        var result = await queryDispatcher.QueryAsync(query);

        return Ok(result);
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<IActionResult> AddProject(ProjectRequestDTO newProjectRequest)
    {
        var command = new RegisterNewProjectCommand(newProjectRequest);

        var newProjectId = await commandDispatcher.DispatchAsync(command);

        if (newProjectId == null) return BadRequest("Registration of the new project failed.");

        var query = new CheckProjectExistsQuery(newProjectId);

        var result = await queryDispatcher.QueryAsync(query);

        return Ok(result);
    }

    [Authorize]
    [HttpPost("update")]
    public async Task<IActionResult> UpdateProject(ProjectRequestDTO updateProjectRequest)
    {
        var command = new UpdateProjectCommand(newProjectRequest);

        var result = await commandDispatcher.DispatchAsync(command);

        return Ok(result);
    }

    [Authorize]
    [HttpPost("remove")]
    public async Task<IActionResult> RemoveProject(string projectId)
    {
        var command = new RemoveProjectCommand(projectId);

        var result = await commandDispatcher.DispatchAsync(command);

        return Ok(result);
    }
}