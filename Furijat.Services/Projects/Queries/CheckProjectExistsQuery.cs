using Furijat.Services.Base.Queries;

namespace Furijat.Services.Projects.Queries;

public record CheckProjectExistsQuery(string ProjectId) : IQuery<bool>;