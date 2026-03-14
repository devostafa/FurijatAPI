using Furijat.Services.Base.Queries;

namespace Furijat.Services.Projects.Queries;

public record GetProjectQuery(int Id) : IQuery<ProjectDto>;