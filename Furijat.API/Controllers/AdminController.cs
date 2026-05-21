using Furijat.Data.Enums;
using Furijat.Data.Repositories.ProjectsRepository;
using Furijat.Services.Base.Commands;
using Furijat.Services.Projects.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Authorize(Policy = "AdminOnly")]
[Route("admin")]
public class AdminController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IProjectsRepository _projectRepo;

    public AdminController(ICommandDispatcher commandDispatcher, IProjectsRepository projectRepo)
    {
        _commandDispatcher = commandDispatcher;
        _projectRepo = projectRepo;
    }

    [HttpGet("project/approve/{projectId}")]
    public async Task<IActionResult> ApproveProject(string projectId)
    {
        var command = new UpdateProjectStatusCommand(projectId, ProjectStatusEnum.Approved);

        var result = await _commandDispatcher.DispatchAsync(command);

        return Ok(result);
    }

    [HttpGet("project/reject/{projectId}")]
    public async Task<IActionResult> RejectProject(string projectId)
    {
        var command = new UpdateProjectStatusCommand(projectId, ProjectStatusEnum.Rejected);

        var result = await _commandDispatcher.DispatchAsync(command);

        return Ok(result);
    }
}