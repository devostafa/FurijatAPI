using Furijat.Data.Enums;
using Furijat.Services.Base.Commands;

namespace Furijat.Services.Projects.Commands;

public record UpdateProjectStatusCommand(string ProjectId, ProjectStatusEnum Status) : ICommand<bool>;