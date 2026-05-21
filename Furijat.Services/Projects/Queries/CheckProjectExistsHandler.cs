using Furijat.Data.Repositories.ProjectsRepository;
using Furijat.Services.Base.Queries;

namespace Furijat.Services.Projects.Queries;

public class CheckProjectExistsHandler(IProjectsRepository projectsRepository) : IQueryHandler<CheckProjectExistsQuery, bool>
{
    public async Task<bool> HandleAsync(CheckProjectExistsQuery query, CancellationToken ct = default)
    {
        return await projectsRepository.CheckProjectExistsAsync(query.ProjectId);
    }
}