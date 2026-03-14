using Furijat.Services.Base.Queries;

namespace Furijat.Services.Projects.Queries;

public class GetProjectHandler : IQueryHandler<GetProjectQuery, ProjectDto>
{

    private readonly IProjectRepository _projectRepository;

    public GetProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository..GetByIdAsync(request.Id);

        var result = 

        return result;
    }
}