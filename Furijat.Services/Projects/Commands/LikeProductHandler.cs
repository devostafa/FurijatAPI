using Furijat.Data.Repositories.ProjectsRepository;
using Furijat.Services.Base.Commands;

namespace Furijat.Services.Projects.Commands;

public class LikeProductHandler : ICommandHandler<LikeProductCommand, bool>
{
    private readonly IProjectsRepository _projectsRepostiroy;

    public LikeProductHandler(IProjectsRepository projectRepository)
    {
        _projectsRepostiroy = projectRepository;
    }

    public async Task<bool> HandleAsync(LikeProductCommand command, CancellationToken ct = default)
    {
        var result = await _projectsRepostiroy.UpdateProjectLikes(command.ProductId);

        return result;
    }
}