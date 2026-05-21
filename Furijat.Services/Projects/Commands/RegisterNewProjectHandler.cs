using Furijat.Data.Repositories.ProjectsRepository;
using Furijat.Services.Base.Commands;

namespace Furijat.Services.Projects.Commands;

public class RegisterNewProjectHandler(IProjectsRepository projectsRepo) : ICommandHandler<RegisterNewProjectCommand, string>
{

    public async Task<string> HandleAsync(RegisterNewProjectCommand command, CancellationToken ct = default)
    {
        var result = await projectsRepo.AddProjectAsync(command.newProjectRequest);

        return result;
    }
}