using Furijat.Data.Repositories.ProjectsRepository;
using Furijat.Services.Base.Commands;

namespace Furijat.Services.Projects.Commands;

public class UpdateProjectStatusHandler : ICommandHandler<UpdateProjectStatusCommand, bool>
{
    private readonly IProjectsRepository _projectRepository;

    public UpdateProjectStatusHandler(IProjectsRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }


    public async Task<bool> HandleAsync(UpdateProjectStatusCommand command, CancellationToken ct = default)
    {
        var result = await _projectRepository.UpdateProjectStatusAsync(command.ProjectId, command.Status);

        return result;
    }
}